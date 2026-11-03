use std::{
    borrow::Cow,
    collections::{BTreeMap, BTreeSet, HashMap},
};

use heck::ToUpperCamelCase;

use crate::{
    bindgen::{
        config::CSharpMarshaller,
        ir::{
            AnnotationSet, Cfg, ConditionWrite as _, ConstExpr, Constant, DeprecatedNoteKind,
            Documentation, Enum, EnumVariant, Field, Function, GenericArgument, GenericPath,
            IntKind, Item, ItemContainer, Literal, LiteralStructField, OpaqueItem, Path,
            PrimitiveType, ReprAlign, Static, Struct, ToCondition, Type, Typedef, Union,
            VariantBody,
        },
        language_backend::LanguageBackend,
        reserved,
        writer::{ListType, SourceWriter},
        DocumentationLength, Layout,
    },
    Bindings, Config,
};

pub struct CSharpLanguageBackend<'a> {
    config: &'a Config,
    class_name: String,
    import_library_expr: String,
    inline_arrays: BTreeMap<Type, BTreeSet<usize>>,
    inline_arrays_constexpr: BTreeMap<Type, BTreeSet<GenericPath>>,
    type_defs: BTreeMap<String, Type>,
    /// Whether the static class for functions and constants has been opened.
    class_open: bool,
    /// Custom marshallers for non-generic keys, by the Rust name of the type.
    marshallers: HashMap<&'a str, &'a CSharpMarshaller>,
    /// Custom marshallers for generic keys, by the Rust name of the generic type and the
    /// number of type parameters, along with the names of the type parameters.
    generic_marshallers: HashMap<(&'a str, usize), (Vec<&'a str>, &'a CSharpMarshaller)>,
    /// The enums of the bindings, by path.
    enums: HashMap<Path, Enum>,
}

/// A custom marshaller applied to a specific type.
struct Marshalling {
    managed: String,
    marshaller: String,
}

impl<'a> CSharpLanguageBackend<'a> {
    pub fn new(config: &'a Config) -> Self {
        let class_name = if config.csharp.class_name.is_empty() {
            if config.csharp.import_library.is_empty() {
                String::from("Api")
            } else {
                config.csharp.import_library.to_upper_camel_case()
            }
        } else {
            config.csharp.class_name.clone()
        };

        let import_library_expr = if config.csharp.import_library.is_empty() {
            String::from("\"library\"")
        } else {
            format!("\"{}\"", config.csharp.import_library.escape_default())
        };

        let mut marshallers = HashMap::new();
        let mut generic_marshallers = HashMap::new();
        for (key, marshaller) in &config.csharp.marshallers {
            match parse_marshaller_key(key) {
                Some((name, params)) if params.is_empty() => {
                    marshallers.insert(name, marshaller);
                }
                Some((name, params)) => {
                    generic_marshallers.insert((name, params.len()), (params, marshaller));
                }
                None => warn!("Ignoring C# marshaller with invalid key {key:?}."),
            }
        }

        CSharpLanguageBackend {
            config,
            class_name,
            import_library_expr,
            inline_arrays: BTreeMap::new(),
            inline_arrays_constexpr: BTreeMap::new(),
            type_defs: BTreeMap::new(),
            class_open: false,
            marshallers,
            generic_marshallers,
            enums: HashMap::new(),
        }
    }

    /// Finds the custom marshaller for a type passed by value to or returned from a function,
    /// looking through type aliases, and substituting the type parameters of generic keys.
    fn marshalling<W: std::io::Write>(
        &mut self,
        out: &SourceWriter<W>,
        ty: &Type,
    ) -> Option<Marshalling> {
        let Type::Path(path) = ty else {
            return None;
        };
        if let Some(marshaller) = self.marshallers.get(path.name()) {
            return Some(Marshalling {
                managed: marshaller.managed.clone(),
                marshaller: marshaller.marshaller.clone(),
            });
        }
        if let Some(aliased) = self.type_defs.get(path.export_name()) {
            let aliased = aliased.clone();
            return self.marshalling(out, &aliased);
        }

        let origin = out.bindings().generic_origins.get(path.path())?;
        let key = (origin.name(), origin.generics().len());
        let (params, marshaller) = self.generic_marshallers.get(&key)?;
        let (params, marshaller): (Vec<&str>, &CSharpMarshaller) = (params.clone(), marshaller);
        let mut arguments = HashMap::new();
        for (param, argument) in params.into_iter().zip(origin.generics()) {
            let GenericArgument::Type(argument) = argument else {
                return None;
            };
            let mut text = Vec::new();
            self.write_type_argument(&mut SourceWriter::new(&mut text, out.bindings()), argument);
            arguments.insert(param, String::from_utf8(text).unwrap());
        }
        Some(Marshalling {
            managed: substitute_identifiers(&marshaller.managed, &arguments),
            marshaller: substitute_identifiers(&marshaller.marshaller, &arguments),
        })
    }

    /// Whether to follow the updated memory safety rules of C# 15, where `unsafe` is a
    /// requirement on callers rather than a scope for using pointers.
    fn updated_memory_safety_rules(&self) -> bool {
        self.config.csharp.updated_memory_safety_rules
    }

    /// Under the legacy memory safety rules, declarations that mention pointers must be
    /// marked `unsafe`. Under the updated rules, `unsafe` is an error on types and would
    /// needlessly require an unsafe context at call sites for other members.
    fn legacy_unsafe(&self, has_pointers: bool) -> &'static str {
        if has_pointers && !self.updated_memory_safety_rules() {
            "unsafe"
        } else {
            ""
        }
    }

    /// The modifiers and keywords declaring a struct, up to its name. Structs with pointer
    /// fields cannot be record structs, because pointers cannot be type arguments.
    fn struct_declaration(&self, has_pointers: bool, record: bool) -> String {
        let mut declaration = String::from("public ");
        let unsaf = self.legacy_unsafe(has_pointers);
        if !unsaf.is_empty() {
            declaration.push_str(unsaf);
            declaration.push(' ');
        }
        declaration.push_str("partial ");
        if record && !has_pointers {
            declaration.push_str("record ");
        }
        declaration.push_str("struct");
        declaration
    }

    /// Writes the documentation and attributes preceding the declaration of a struct or union
    /// with the given `layout`. Returns `false` after writing a comment instead if it cannot be
    /// represented in C#.
    #[allow(clippy::too_many_arguments)]
    fn write_struct_attributes<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        kind: &str,
        name: &str,
        alignment: &Option<ReprAlign>,
        documentation: &Documentation,
        annotations: &AnnotationSet,
        layout: &str,
    ) -> bool {
        let pack = match alignment {
            Some(ReprAlign::Packed) => ", Pack = 1",
            Some(ReprAlign::Align(_)) => {
                write!(
                    out,
                    "// cbindgen: Ignoring {kind} {name} because it has alignment attributes"
                );
                return false;
            }
            None => "",
        };
        self.write_documentation(out, documentation);
        self.write_obsolete_attribute(out, annotations, DeprecatedNoteKind::Struct);
        write!(out, "[StructLayout(LayoutKind.{layout}{pack})]");
        out.new_line();
        true
    }

    /// Under the updated memory safety rules, fields of explicit layout structs must be
    /// marked either `safe` or `unsafe`.
    fn explicit_layout_field_modifier(&self, is_safe: bool) -> &'static str {
        match (self.updated_memory_safety_rules(), is_safe) {
            (false, _) => "",
            (true, true) => "safe ",
            (true, false) => "unsafe ",
        }
    }

    /// Writes a type, except that `bool` is written as `bool` instead of its blittable
    /// representation. Struct fields of type `bool` are exposed as `bool` properties, and
    /// marshalling converts `bool` in signatures.
    fn write_bool_type<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, ty: &Type) {
        if let Type::Primitive(PrimitiveType::Bool) = ty {
            out.write("bool");
        } else {
            self.write_type(out, ty);
        }
    }

    /// Writes a type where pointers are not allowed, such as type arguments and the targets of
    /// type aliases, writing pointers as `nint`.
    fn write_type_argument<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, ty: &Type) {
        if ty.is_ptr() {
            out.write("nint");
        } else {
            self.write_type(out, ty);
        }
    }

    /// Writes an `[Obsolete]` attribute if the annotations mark the item as deprecated.
    fn write_obsolete_attribute<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        annotations: &AnnotationSet,
        kind: DeprecatedNoteKind,
    ) {
        if let Some(note) = annotations.deprecated_note(self.config, kind) {
            write!(out, "[System.Obsolete(\"{}\")]", note.escape_default());
            out.new_line();
        }
    }

    /// Prepares for writing a member of the static class, which is opened by the first member,
    /// so that it is omitted when empty. Members are separated by a blank line, and must leave
    /// the writer at the end of their last line.
    fn begin_class_member<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>) {
        if self.class_open {
            out.new_line();
            out.new_line();
        } else {
            out.new_line_if_not_start();
            write!(out, "public static partial class {}", self.class_name);
            out.open_brace();
            self.class_open = true;
        }
    }

    fn close_class<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>) {
        if self.class_open {
            out.close_brace(false);
            out.new_line();
            self.class_open = false;
        }
    }

    fn write_enum_variant<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        u: &EnumVariant,
    ) {
        let condition = u.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        self.write_documentation(out, &u.documentation);
        self.write_obsolete_attribute(out, u.body.annotations(), DeprecatedNoteKind::EnumVariant);
        write!(out, "{}", u.export_name);
        if let Some(discriminant) = &u.discriminant {
            out.write(" = ");
            self.write_literal(out, discriminant);
        }
        out.write(",");
        condition.write_after(self.config, out);
    }

    /// The fields of the struct representing `variant`, excluding the inline tag. Tuple variant
    /// fields are named like C# tuples: `Value` for a single field, and `Item1`, `Item2`, etc.
    /// otherwise.
    fn variant_fields(e: &Enum, variant: &EnumVariant) -> Vec<Field> {
        let mut fields = Self::ir_variant_fields(e, variant).to_vec();
        if let VariantBody::Body { is_tuple: true, .. } = variant.body {
            let single = fields.len() == 1;
            for (i, field) in fields.iter_mut().enumerate() {
                field.name = if single {
                    "Value".to_owned()
                } else {
                    format!("Item{}", i + 1)
                };
            }
        }
        fields
    }

    /// Writes the creation of an object of type `type_name` with an object initializer, where
    /// `members` are the literal field name, the C# member name and the type of each member.
    fn write_object_creation<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        type_name: &str,
        members: &[(String, String, Type)],
        fields: &HashMap<String, LiteralStructField>,
    ) {
        write!(out, "new {type_name}");
        if fields.is_empty() {
            out.write("()");
            return;
        }
        out.open_brace();
        for (i, (key, member, ty)) in members.iter().enumerate() {
            if i > 0 {
                out.new_line();
            }
            if let Some(lit) = fields.get(key) {
                let condition = lit.cfg.to_condition(self.config);
                condition.write_before(self.config, out);
                write!(out, "{member} = (");
                self.write_bool_type(out, ty);
                out.write(")");
                self.write_literal(out, &lit.value);
                out.write(",");
                condition.write_after(self.config, out);
            } else {
                write!(out, "// cbindgen: missing field: {member}");
            }
        }
        out.close_brace(false);
    }

    /// The fields of `variant` as in the IR, without the tag, where tuple fields are named `_0`,
    /// `_1`, etc.
    fn ir_variant_fields<'e>(e: &Enum, variant: &'e EnumVariant) -> &'e [Field] {
        match &variant.body {
            VariantBody::Body { body, .. } if Enum::inline_tag_field(&e.repr) => &body.fields[1..],
            VariantBody::Body { body, .. } => &body.fields,
            VariantBody::Empty(..) => &[],
        }
    }

    /// The names of the members of the data enum `e`, other than associated constants.
    fn data_enum_member_names(e: &Enum) -> Vec<String> {
        let mut names: Vec<String> = DATA_ENUM_MEMBERS
            .iter()
            .map(|&name| name.to_owned())
            .collect();
        names.push(e.tag_name().to_owned());
        names.push(Self::data_struct_name(e));
        names.extend(
            e.variants
                .iter()
                .map(|variant| Self::variant_type_name(e, variant)),
        );
        names
    }

    /// The C# name of the associated constant `name` of the type at `path`. Constants associated
    /// with a data enum are renamed if they collide with another member of the enum.
    fn associated_constant_name(&self, path: &Path, owner: &str, name: &str) -> String {
        let mut member_name = associated_member_name(owner, name);
        if let Some(e) = self.enums.get(path).filter(|e| e.tag.is_some()) {
            let reserved = Self::data_enum_member_names(e);
            while reserved.contains(&member_name) {
                member_name.push('_');
            }
        }
        member_name
    }

    /// Whether `constant` is a member of the struct or data enum that it is associated with,
    /// rather than of the static class.
    fn is_member_constant(&self, bindings: &Bindings, constant: &Constant) -> bool {
        constant.associated_to.as_ref().is_some_and(|path| {
            bindings.struct_exists(path) || self.enums.get(path).is_some_and(|e| e.tag.is_some())
        })
    }

    /// The associated constants of the type at `path`. The parser attaches associated constants
    /// to structs, unless the `impl` block is parsed before the struct, e.g. when the struct is
    /// in a nested module, which makes them regular constants. Associated constants of enums are
    /// always regular constants.
    fn associated_constants(
        bindings: &Bindings,
        path: &Path,
        attached: &[Constant],
    ) -> Vec<Constant> {
        let regular = bindings
            .constants
            .iter()
            .filter(|constant| constant.associated_to.as_ref() == Some(path));
        attached.iter().chain(regular).cloned().collect()
    }

    /// Parameter names follow the field names, except that the PascalCase names of tuple
    /// fields are camelCased.
    fn variant_parameter_name(variant: &EnumVariant, field: &Field) -> String {
        match variant.body {
            VariantBody::Body { is_tuple: true, .. } => {
                let mut name = field.name.clone();
                name[..1].make_ascii_lowercase();
                name
            }
            _ => field.name.clone(),
        }
    }

    /// The name of the nested struct representing `variant` of the data enum `e`, which is the
    /// Rust name of the variant, unless that would collide with another member of the enum.
    fn variant_type_name(e: &Enum, variant: &EnumVariant) -> String {
        let mut name = variant.name.clone();
        reserved::escape_csharp(&mut name);
        let data_struct_name = Self::data_struct_name(e);
        let collides = name == e.export_name()
            || name == e.tag_name()
            || name == data_struct_name
            || DATA_ENUM_MEMBERS.contains(&name.as_str())
            || Self::variant_fields(e, variant)
                .iter()
                .any(|field| field.name == name)
            // With an inline tag, the variant bodies are fields of the enum itself.
            || (Enum::inline_tag_field(&e.repr)
                && e.variants.iter().any(|v| {
                    matches!(&v.body, VariantBody::Body { name: field, .. } if *field == name)
                }));
        if collides {
            name.push('_');
        }
        name
    }

    fn write_enum_data_variant<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        e: &Enum,
        variant: &EnumVariant,
    ) {
        let tag_name = e.tag_name();
        let inline_tag_field = Enum::inline_tag_field(&e.repr);
        let type_name = Self::variant_type_name(e, variant);
        let condition = variant.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        self.write_documentation(out, &variant.documentation);
        self.write_obsolete_attribute(
            out,
            variant.body.annotations(),
            DeprecatedNoteKind::EnumVariant,
        );
        let VariantBody::Body { ref body, .. } = variant.body else {
            // Variants without a body are never stored, but still need a type to be
            // constructed from and matched against.
            write!(out, "public partial record struct {type_name}();");
            condition.write_after(self.config, out);
            return;
        };
        out.write("[StructLayout(LayoutKind.Sequential)]");
        out.new_line();
        let has_pointers = body.fields.iter().any(|field| field.ty.is_ptr());
        let declaration = self.struct_declaration(has_pointers, true);
        write!(out, "{declaration} {type_name}()");
        out.open_brace();
        if inline_tag_field {
            write!(
                out,
                "private readonly {tag_name} _tag = {tag_name}.{};",
                variant.export_name
            );
            out.new_line();
        }
        let fields = Self::variant_fields(e, variant);
        out.write_vertical_source_list(
            self,
            &fields,
            ListType::Cap(""),
            Self::write_record_struct_field,
        );
        out.new_line();
        if !fields.is_empty() {
            out.new_line();
            out.write("[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]");
            out.new_line();
            write!(out, "public {type_name}(");
            for (i, field) in fields.iter().enumerate() {
                if i > 0 {
                    out.write(", ");
                }
                self.write_bool_type(out, &field.ty);
                write!(out, " {}", Self::variant_parameter_name(variant, field));
            }
            out.write(") : this()");
            out.open_brace();
            for (i, field) in fields.iter().enumerate() {
                if i > 0 {
                    out.new_line();
                }
                write!(
                    out,
                    "this.{} = {};",
                    field.name,
                    Self::variant_parameter_name(variant, field)
                );
            }
            out.close_brace(false);
            out.new_line();
        }
        if has_pointers {
            // This is not a record struct, so we need to implement `PrintMembersInternal` manually.
            write!(out, "internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => stringBuilder.Append(\"...\");");
        } else {
            // This is a record struct, so we can use the automatically generated `PrintMembers` method.
            write!(out, "internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);");
        }
        out.close_brace(false);
        condition.write_after(self.config, out);
    }

    fn write_record_struct_field<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        f: &Field,
    ) {
        let condition = f.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        self.write_documentation(out, &f.documentation);
        out.write("public required ");
        if let Type::Primitive(PrimitiveType::Bool) = f.ty {
            // Bools are a single byte in Rust, but 4 bytes in .NET, so we provide a property.
            // Representing structs this way is required for the struct to be blittable.
            write!(
                out,
                "bool {name} {{ readonly get => _{name} != 0; set => _{name} = value ? (byte)1 : (byte)0; }}",
                name = f.name
            );
            out.new_line();
            write!(out, "private byte _{name}", name = f.name);
        } else {
            self.write_type(out, &f.ty);
            write!(out, " {}", f.name);
        }
        out.write(";");
        condition.write_after(self.config, out);
    }

    fn write_union_struct_field<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        f: &Field,
    ) {
        let condition = f.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        self.write_documentation(out, &f.documentation);
        out.write("[FieldOffset(0)]");
        out.new_line();
        // Like in Rust, reading a union field is unsafe, since nothing guarantees that the field
        // was the one last written.
        write!(out, "public {}", self.explicit_layout_field_modifier(false));
        self.write_type(out, &f.ty);
        write!(out, " {}", f.name);
        out.write(";");
        condition.write_after(self.config, out);
    }

    fn write_inline_arrays<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>) {
        for (ty, sizes) in std::mem::take(&mut self.inline_arrays) {
            let name = self.get_mangled_type_name(&ty);
            // Smaller arrays use the built-in `InlineArrayN<T>` types.
            for size in sizes.into_iter().filter(|&size| size > 16) {
                let length = size.to_string();
                self.write_inline_array(out, &length, &format!("InlineArray{size}_{name}"), &ty);
            }
        }

        for (ty, size_names) in std::mem::take(&mut self.inline_arrays_constexpr) {
            let name = self.get_mangled_type_name(&ty);
            for size_name in size_names {
                let size_name = size_name.export_name();
                let length = format!("(int){}.{size_name}", self.class_name);
                self.write_inline_array(
                    out,
                    &length,
                    &format!("InlineArray_{size_name}_{name}"),
                    &ty,
                );
            }
        }
    }

    fn write_inline_array<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        length: &str,
        name: &str,
        ty: &Type,
    ) {
        write!(
            out,
            "[System.Runtime.CompilerServices.InlineArray({length})]"
        );
        out.new_line();
        write!(out, "public struct {name}");
        out.open_brace();
        out.write("private ");
        self.write_type_argument(out, ty);
        out.write(" _data;");
        out.close_brace(false);
        out.new_line();
    }

    fn write_tag_enum<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, e: &Enum) {
        match e.repr.ty.map(|ty| ty.to_primitive()) {
            Some(
                prim @ PrimitiveType::Integer {
                    kind: IntKind::Size | IntKind::SizeT,
                    ..
                },
            ) => {
                // C# can't use `IntPtr` or `UIntPtr` as the representation type of an enum, so we
                // emulate it with a record struct.
                let condition = e.cfg.to_condition(self.config);
                condition.write_before(self.config, out);
                self.write_documentation(out, &e.documentation);
                let size = prim.to_repr_csharp();
                out.write("[StructLayout(LayoutKind.Sequential)]");
                out.new_line();
                write!(
                    out,
                    "public readonly record struct {}({size} RawValue)",
                    e.export_name
                );
                out.open_brace();
                let mut previous_variant = None::<&EnumVariant>;
                for (i, variant) in e.variants.iter().enumerate() {
                    if i > 0 {
                        out.new_line();
                    }
                    self.write_documentation(out, &variant.documentation);
                    if let Some(ref discriminant) = variant.discriminant {
                        write!(
                            out,
                            "public static {} {} => new(",
                            e.export_name, variant.export_name
                        );
                        self.write_literal(out, discriminant);
                        write!(out, ");");
                    } else if let Some(previous_variant) = previous_variant {
                        // TODO: This breaks down when the previous variant was conditionally
                        // compiled.
                        write!(
                            out,
                            "public static {} {} => new({}.RawValue + 1);",
                            e.export_name, variant.export_name, previous_variant.export_name
                        );
                    } else {
                        // First variant, no discriminant, set to zero.
                        write!(
                            out,
                            "public static {} {} => new(0);",
                            e.export_name, variant.export_name
                        );
                    }
                    previous_variant = Some(variant);
                }
                out.close_brace(false);
                condition.write_after(self.config, out);
            }
            Some(prim) => {
                let size = prim.to_repr_csharp();
                e.write_tag_enum(self.config, self, out, Some(size), Self::write_enum_variant);
            }
            None => e.write_tag_enum(self.config, self, out, None, Self::write_enum_variant),
        }
    }

    fn get_mangled_type_name(&self, ty: &Type) -> Cow<'static, str> {
        match ty {
            Type::Ptr {
                ty,
                is_const,
                is_nullable,
                is_ref,
            } => {
                let prefix = match (is_const, is_nullable, is_ref) {
                    (true, true, true) => "_const_ref__",
                    (true, true, false) => "_const_ptr__",
                    (true, false, true) => "_const_nonnull_ref__",
                    (true, false, false) => "_const_nonnull_ptr__",
                    (false, true, true) => "_ref__",
                    (false, true, false) => "_ptr__",
                    (false, false, true) => "_nonnull_ref__",
                    (false, false, false) => "_nonnull_ptr__",
                };
                format!("{prefix}{}", self.get_mangled_type_name(ty)).into()
            }
            Type::Path(generic_path) => generic_path.path().name().to_string().into(),
            Type::Primitive(primitive_type) => primitive_type.to_repr_csharp().into(),
            Type::Array(ty, const_expr) => format!(
                "InlineArray{}_{}",
                const_expr.as_str(),
                self.get_mangled_type_name(ty)
            )
            .into(),
            Type::FuncPtr {
                ret,
                args,
                is_nullable,
                never_return,
            } => {
                let prefix = match (is_nullable, never_return) {
                    (true, true) => "_funcptr_nullable_never_return__",
                    (true, false) => "_funcptr_nullable__",
                    (false, true) => "_funcptr_never_return__",
                    (false, false) => "_funcptr__",
                };
                let mut s = format!("{prefix}{}", self.get_mangled_type_name(ret));
                for arg in args {
                    s.push('_');
                    s.push_str(&self.get_mangled_type_name(&arg.1));
                }
                s.into()
            }
        }
    }

    fn write_type_defs<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, b: &Bindings) {
        for item in &b.items {
            if let ItemContainer::Typedef(typedef) = item {
                self.write_type_def(out, typedef);
                out.new_line();
            }
        }
    }

    /// Every enum that has `cbindgen:enum-class=false` may have its variants used as global aliases.
    /// There is no equivalent of this in C#, where all enums are scoped, so we emulate this by defining
    /// class constants. It is necessary to do this to support the expansion of literals that may refer
    /// to such global aliases (when there is also a `use MyEnum::*` on the Rust side).
    fn write_enum_global_aliases<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        b: &Bindings,
    ) {
        for item in &b.items {
            if let ItemContainer::Enum(enum_) = item {
                if enum_
                    .annotations
                    .bool("enum-class")
                    .is_some_and(|enum_class| !enum_class)
                {
                    let enum_condition = enum_.cfg.to_condition(self.config);
                    for variant in &enum_.variants {
                        self.begin_class_member(out);
                        enum_condition.write_before(self.config, out);
                        let condition = variant.cfg.to_condition(self.config);
                        condition.write_before(self.config, out);
                        write!(
                            out,
                            "public const {} {} = {}.{};",
                            enum_.export_name, variant.export_name, enum_.export_name, variant.name
                        );
                        condition.write_after(self.config, out);
                        enum_condition.write_after(self.config, out);
                    }
                }
            }
        }
    }

    /// Writes a constant, leaving the writer at the end of the last line written.
    fn write_constant<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        constant: &Constant,
        associated_to: Option<&Struct>,
    ) {
        let Some(associated_to) = associated_to else {
            if constant.uses_only_primitive_types() {
                constant.write(self.config, self, out, None);
                return;
            }
            self.write_constant_member(out, constant, &constant.export_name);
            return;
        };
        // Associated constants are members of their struct, so they follow the C# naming
        // conventions for members.
        let name = associated_member_name(associated_to.export_name(), &constant.export_name);
        self.write_constant_member(out, constant, &name);
    }

    fn write_constant_member<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        constant: &Constant,
        name: &str,
    ) {
        if !constant.value.is_valid(out.bindings()) {
            write!(
                out,
                "// cbindgen: Skipped '{}', because one of its required types is not included",
                constant.name()
            );
            return;
        }
        if constant.value.has_pointer_casts() {
            write!(
                out,
                "// cbindgen: Skipped '{}' because it contains pointer casts",
                constant.name()
            );
            return;
        }
        let condition = constant.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        if let Type::Array(ref elem, _) = constant.ty {
            self.write_array_constant(out, constant, name, elem);
        } else if let Type::Primitive(_) = constant.ty {
            self.write_documentation(out, &constant.documentation);
            out.write("public const ");
            self.write_type(out, &constant.ty);
            write!(out, " {name} = (");
            self.write_type(out, &constant.ty);
            out.write(")");
            self.write_literal(out, &constant.value);
            out.write(";");
        } else {
            // Non-primitive constants cannot be `const`. Properties are preferred over
            // `static readonly` fields, which are initialized in declaration order, so they could
            // observe other constants before their initialization.
            self.write_documentation(out, &constant.documentation);
            out.write("public static ");
            self.write_type(out, &constant.ty);
            write!(out, " {name} => ");
            self.write_literal(out, &constant.value);
            out.write(";");
        }
        condition.write_after(self.config, out);
    }

    /// Array constants (including string and byte-string constants, which are `char[]` and
    /// `uint8_t[N]` in the IR) cannot be `const` in C#, and collection expressions cannot
    /// target inline array types, so they are exposed as `ReadOnlySpan<T>` properties, which
    /// the compiler backs by static data.
    fn write_array_constant<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        constant: &Constant,
        name: &str,
        elem: &Type,
    ) {
        // String literals are emitted as C string literals by the IR.
        if let Literal::Expr(ref v) = constant.value {
            if let Some(bytes) = decode_c_string_literal(v) {
                self.write_documentation(out, &constant.documentation);
                write!(out, "public static System.ReadOnlySpan<byte> {} => ", name);
                match std::str::from_utf8(&bytes) {
                    Ok(s) => write!(out, "{}u8;", to_csharp_string_literal(s)),
                    Err(_) => {
                        // UTF-8 string literals cannot hold invalid UTF-8, so fall back to the
                        // raw bytes.
                        out.write("[");
                        for (i, byte) in bytes.iter().enumerate() {
                            if i > 0 {
                                out.write(", ");
                            }
                            write!(out, "0x{byte:02X}");
                        }
                        out.write("];");
                    }
                }
                return;
            }
        }

        // Pointers cannot be type arguments, and nested arrays would need inline array element
        // types, which collection expressions cannot construct.
        if !matches!(elem, Type::Primitive(_) | Type::Path(_)) {
            write!(
                out,
                "// cbindgen: Skipped '{}' because its element type cannot be represented in a C# constant",
                name
            );
            return;
        }

        self.write_documentation(out, &constant.documentation);
        out.write("public static System.ReadOnlySpan<");
        self.write_type(out, elem);
        write!(out, "> {} => ", name);
        self.write_literal(out, &constant.value);
        out.write(";");
    }

    /// The implementation of a data enum necessarily refers to its deprecated variants, which
    /// must not cause warnings in the bindings themselves. Warnings are still reported for uses
    /// of the deprecated variants outside of the bindings.
    fn write_data_enum_obsolete_pragma<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        e: &Enum,
        disable: bool,
    ) {
        let has_deprecated_variants = e.variants.iter().any(|variant| {
            variant
                .body
                .annotations()
                .deprecated_note(self.config, DeprecatedNoteKind::EnumVariant)
                .is_some()
        });
        if !has_deprecated_variants {
            return;
        }
        if disable {
            out.write("#pragma warning disable CS0612, CS0618");
            out.new_line();
        } else {
            out.new_line();
            out.write("#pragma warning restore CS0612, CS0618");
        }
    }

    /// Writes the tag enum of a data enum. The variants are documented on the variant structs,
    /// which users interact with, so the documentation is not repeated on the tag values.
    fn write_data_enum_tag<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, e: &Enum) {
        let mut e = e.clone();
        for variant in &mut e.variants {
            variant.documentation = Documentation::none();
        }
        self.write_tag_enum(out, &e);
    }

    fn write_data_enum<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, e: &Enum) {
        let inline_tag_field = Enum::inline_tag_field(&e.repr);
        let tag_name = e.tag_name();
        // With an inline tag, each variant struct starts with the tag, so the variants overlap the
        // tag. Otherwise, the tag is followed by a separate union of the variants.
        let layout = if inline_tag_field {
            "Explicit"
        } else {
            "Sequential"
        };
        write!(out, "[StructLayout(LayoutKind.{layout})]");
        out.new_line();
        self.write_union_attribute(out, e);
        write!(
            out,
            "public partial struct {name} : System.IEquatable<{name}>",
            name = e.export_name()
        );
        out.open_brace();
        self.write_data_enum_obsolete_pragma(out, e, true);
        self.write_data_enum_tag(out, e);
        out.new_line();
        // The overlapping fields are private, and only accessed after checking the tag.
        let safe = self.explicit_layout_field_modifier(true);
        if inline_tag_field {
            out.write("[FieldOffset(0)]");
            out.new_line();
            write!(out, "private readonly {safe}{tag_name} _tag;");
            out.new_line();
            for variant in &e.variants {
                let VariantBody::Body { name, .. } = &variant.body else {
                    continue;
                };
                self.write_conditional_line(out, &variant.cfg, |out| {
                    out.write("[FieldOffset(0)]");
                    out.new_line();
                    let type_name = Self::variant_type_name(e, variant);
                    write!(out, "private {safe}{type_name} {name};");
                });
            }
            out.new_line();
            self.write_data_enum_members(out, e);
        } else {
            let data_struct_name = Self::data_struct_name(e);
            write!(out, "private readonly {tag_name} _tag;");
            out.new_line();
            write!(out, "private {data_struct_name} _data;");
            out.new_line();
            out.new_line();
            self.write_data_enum_members(out, e);
            out.new_line();
            out.new_line();
            out.write("[StructLayout(LayoutKind.Explicit)]");
            out.new_line();
            write!(out, "private struct {data_struct_name}");
            out.open_brace();
            let mut first = true;
            for variant in &e.variants {
                let VariantBody::Body { name, .. } = &variant.body else {
                    continue;
                };
                if !first {
                    out.new_line();
                }
                first = false;
                let condition = variant.cfg.to_condition(self.config);
                condition.write_before(self.config, out);
                out.write("[FieldOffset(0)]");
                out.new_line();
                let type_name = Self::variant_type_name(e, variant);
                write!(out, "public {safe}{type_name} {name};");
                condition.write_after(self.config, out);
            }
            out.close_brace(false);
        }
        for variant in &e.variants {
            out.new_line();
            out.new_line();
            self.write_enum_data_variant(out, e, variant);
        }
        self.write_data_enum_obsolete_pragma(out, e, false);
        out.close_brace(false);
    }

    /// The name of the private union of the variants of a data enum with a separate tag.
    fn data_struct_name(e: &Enum) -> String {
        format!("{}_Data", e.export_name())
    }

    /// The prefix for accessing the private field of a variant from the data enum.
    fn variant_field_prefix(e: &Enum) -> &'static str {
        if Enum::inline_tag_field(&e.repr) {
            ""
        } else {
            "_data."
        }
    }

    /// Writes a line, or several, wrapped in the condition of `cfg`, followed by a new line.
    fn write_conditional_line<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        cfg: &Option<Cfg>,
        write: impl FnOnce(&mut SourceWriter<W>),
    ) {
        let condition = cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        write(out);
        condition.write_after(self.config, out);
        out.new_line();
    }

    fn write_data_enum_members<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, e: &Enum) {
        for constant in &Self::associated_constants(out.bindings(), &e.path, &[]) {
            let name =
                self.associated_constant_name(&e.path, e.export_name(), &constant.export_name);
            self.write_constant_member(out, constant, &name);
            out.new_line();
            out.new_line();
        }
        self.write_data_enum_constructors(out, e);
        out.new_line();
        self.write_data_enum_implicit_conversion_operators(out, e);
        out.new_line();
        write!(
            out,
            "public readonly {} Discriminant => _tag;",
            e.tag_name()
        );
        out.new_line();
        if self.config.csharp.union_types {
            self.write_data_enum_union_members(out, e);
        }
        self.write_data_enum_try_get_value(out, e);
        out.new_line();
        self.write_data_enum_equals(out, e);
        out.new_line();
        self.write_data_enum_get_hash_code(out, e);
        out.new_line();
        self.write_data_enum_to_string(out, e);
    }

    /// Writes a constructor taking each variant struct.
    fn write_data_enum_constructors<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        e: &Enum,
    ) {
        let tag_name = e.tag_name();
        let inline_tag_field = Enum::inline_tag_field(&e.repr);
        for variant in &e.variants {
            let type_name = Self::variant_type_name(e, variant);
            self.write_conditional_line(out, &variant.cfg, |out| {
                match &variant.body {
                    VariantBody::Empty(_) => {
                        write!(out, "public {}({type_name} _)", e.export_name);
                        out.open_brace();
                        write!(out, "_tag = {tag_name}.{};", variant.export_name);
                    }
                    VariantBody::Body { name, .. } => {
                        write!(out, "public {}({type_name} {name})", e.export_name);
                        out.open_brace();
                        if inline_tag_field {
                            // The tag is part of the variant struct.
                            write!(out, "this.{name} = {name};");
                        } else {
                            write!(out, "_tag = {tag_name}.{};", variant.export_name);
                            out.new_line();
                            write!(out, "_data.{name} = {name};");
                        }
                    }
                }
                out.close_brace(false);
            });
        }
    }

    fn write_data_enum_implicit_conversion_operators<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        e: &Enum,
    ) {
        for variant in &e.variants {
            self.write_conditional_line(out, &variant.cfg, |out| {
                write!(
                    out,
                    "public static implicit operator {}({} value) => new(value);",
                    e.export_name,
                    Self::variant_type_name(e, variant)
                );
            });
        }
    }

    fn write_union_attribute<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, e: &Enum) {
        if !self.config.csharp.union_types {
            return;
        }
        // A union type must have at least one case type, so it is only a union if at least one
        // variant is compiled in.
        let mut cfgs = Vec::new();
        for variant in &e.variants {
            match variant.cfg {
                Some(ref cfg) => cfgs.push(cfg.clone()),
                None => {
                    cfgs.clear();
                    break;
                }
            }
        }
        let cfg = (!cfgs.is_empty()).then_some(Cfg::Any(cfgs));
        let condition = cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        out.write("[System.Runtime.CompilerServices.Union]");
        condition.write_after(self.config, out);
        out.new_line();
    }

    /// Writes the members of the custom union type pattern besides `TryGetValue`. The case
    /// types are the variant structs, which are defined by the single-parameter constructors.
    fn write_data_enum_union_members<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        e: &Enum,
    ) {
        let tag_name = e.tag_name();
        let prefix = Self::variant_field_prefix(e);

        out.write("public readonly object? Value => _tag switch");
        out.open_brace();
        for variant in &e.variants {
            self.write_conditional_line(out, &variant.cfg, |out| {
                write!(out, "{tag_name}.{} => ", variant.export_name);
                match &variant.body {
                    VariantBody::Empty(_) => {
                        write!(out, "new {}(),", Self::variant_type_name(e, variant))
                    }
                    VariantBody::Body { name, .. } => write!(out, "{prefix}{name},"),
                }
            });
        }
        // Only reachable with an invalid tag.
        out.write("_ => null,");
        out.close_brace(false);
        out.write(";");
        out.new_line();

        // Allows the compiler to match `null` without boxing the value.
        out.write("public readonly bool HasValue => _tag switch");
        out.open_brace();
        for variant in &e.variants {
            self.write_conditional_line(out, &variant.cfg, |out| {
                write!(out, "{tag_name}.{} => true,", variant.export_name);
            });
        }
        out.write("_ => false,");
        out.close_brace(false);
        out.write(";");
        out.new_line();
    }

    /// Writes a `TryGetValue` method for each variant, which union types also use to match the
    /// case types without boxing the value.
    fn write_data_enum_try_get_value<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        e: &Enum,
    ) {
        let tag_name = e.tag_name();
        let prefix = Self::variant_field_prefix(e);
        for variant in &e.variants {
            self.write_conditional_line(out, &variant.cfg, |out| {
                write!(
                    out,
                    "public readonly bool TryGetValue(out {} value)",
                    Self::variant_type_name(e, variant)
                );
                out.open_brace();
                match &variant.body {
                    VariantBody::Empty(_) => out.write("value = default;"),
                    VariantBody::Body { name, .. } => write!(
                        out,
                        "value = _tag == {tag_name}.{} ? {prefix}{name} : default;",
                        variant.export_name
                    ),
                }
                out.new_line();
                write!(out, "return _tag == {tag_name}.{};", variant.export_name);
                out.close_brace(false);
            });
        }
    }

    fn write_data_enum_equals<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, e: &Enum) {
        let tag_name = e.tag_name();
        let prefix = Self::variant_field_prefix(e);
        write!(out, "public override readonly bool Equals(object? obj) => obj is {} other && Equals(other);", e.export_name);
        out.new_line();
        write!(out, "public readonly bool Equals({} other)", e.export_name);
        out.open_brace();
        write!(out, "if (_tag != other._tag) return false;");
        out.new_line();
        write!(out, "return _tag switch");
        out.open_brace();
        for variant in &e.variants {
            let VariantBody::Body { name, .. } = &variant.body else {
                continue;
            };
            self.write_conditional_line(out, &variant.cfg, |out| {
                write!(
                    out,
                    "{tag_name}.{} => {prefix}{name}.Equals(other.{prefix}{name}),",
                    variant.export_name
                );
            });
        }
        write!(out, "_ => true,");
        out.close_brace(true); // switch
        out.close_brace(false);
        out.new_line();
        write!(
            out,
            "public static bool operator ==({ename} left, {ename} right) => left.Equals(right);",
            ename = e.export_name
        );
        out.new_line();
        write!(
            out,
            "public static bool operator !=({ename} left, {ename} right) => !left.Equals(right);",
            ename = e.export_name
        );
        out.new_line();
    }

    fn write_data_enum_get_hash_code<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        e: &Enum,
    ) {
        let tag_name = e.tag_name();
        let prefix = Self::variant_field_prefix(e);
        write!(out, "public override readonly int GetHashCode()");
        out.open_brace();
        write!(out, "var hashCode = new System.HashCode();");
        out.new_line();
        // With an inline tag, the tag is hashed as part of the variant struct.
        if !Enum::inline_tag_field(&e.repr) {
            write!(out, "hashCode.Add(_tag);");
            out.new_line();
        }
        write!(out, "switch (_tag)");
        out.open_brace();
        for variant in &e.variants {
            let VariantBody::Body { name, .. } = &variant.body else {
                continue;
            };
            self.write_conditional_line(out, &variant.cfg, |out| {
                write!(out, "case {tag_name}.{}:", variant.export_name);
                out.open_brace();
                write!(out, "hashCode.Add({prefix}{name});");
                out.new_line();
                write!(out, "break;");
                out.close_brace(false);
            });
        }
        write!(out, "default: break;");
        out.close_brace(false); // switch
        out.new_line();
        write!(out, "return hashCode.ToHashCode();");
        out.close_brace(false);
        out.new_line();
    }

    fn write_data_enum_to_string<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        e: &Enum,
    ) {
        let tag_name = e.tag_name();
        let prefix = Self::variant_field_prefix(e);
        write!(out, "public override readonly string ToString()");
        out.open_brace();
        write!(out, "var stringBuilder = new System.Text.StringBuilder();");
        out.new_line();
        // Note: .NET by default does not include the enum's own name in the ToString output, so we only print the tag name as well.
        write!(out, "stringBuilder.Append(_tag.ToString());");
        out.new_line();

        write!(out, "switch (_tag)");
        out.open_brace();
        for variant in &e.variants {
            let VariantBody::Body { name, .. } = &variant.body else {
                continue;
            };
            self.write_conditional_line(out, &variant.cfg, |out| {
                write!(out, "case {tag_name}.{}:", variant.export_name);
                out.open_brace();
                write!(out, "stringBuilder.Append(\" {{ \");");
                out.new_line();
                write!(out, "{prefix}{name}.PrintMembersInternal(stringBuilder);");
                out.new_line();
                write!(out, "stringBuilder.Append(\" }}\");");
                out.new_line();
                write!(out, "break;");
                out.close_brace(false);
            });
        }
        write!(out, "default: break;");
        out.close_brace(false); // switch
        out.new_line();
        write!(out, "return stringBuilder.ToString();");
        out.close_brace(false);
    }

    fn write_bitflags_enum<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, s: &Struct) {
        // "Re-sugar" a bitflags struct as a C# enum with the `[Flags]` attribute.
        let condition = s.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        self.write_documentation(out, &s.documentation);

        // Find the repr from the synthesized "bits" field.
        let Some(bits_field) = s.fields.first() else {
            panic!("struct {} does not have a 'bits' field", s.export_name,);
        };
        let Type::Primitive(repr) = &bits_field.ty else {
            panic!("{}.bits is not a primitive type", s.export_name);
        };

        out.write("[System.Flags]");
        out.new_line();
        write!(
            out,
            "public enum {} : {}",
            s.export_name,
            repr.to_repr_csharp()
        );
        out.open_brace();

        // Bitflags are associated constants.
        let constants =
            Self::associated_constants(out.bindings(), &s.path, &s.associated_constants);
        for (i, constant) in constants.iter().enumerate() {
            if i != 0 {
                out.new_line();
            }
            let condition = constant.cfg.to_condition(self.config);
            condition.write_before(self.config, out);
            self.write_documentation(out, &constant.documentation);
            write!(
                out,
                "{} = ",
                associated_member_name(&s.export_name, &constant.export_name)
            );
            self.write_literal(out, &constant.value);
            out.write(",");
            condition.write_after(self.config, out);
        }

        out.close_brace(false);

        condition.write_after(self.config, out);
    }
}

impl LanguageBackend for CSharpLanguageBackend<'_> {
    fn open_namespaces<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>) {
        out.write("using System.Runtime.InteropServices;");
        out.new_line();
        if !self.config.csharp.marshallers.is_empty() {
            out.write("using System.Runtime.InteropServices.Marshalling;");
            out.new_line();
        }
        for using_namespace in &self.config.csharp.using_namespaces {
            write!(out, "using {};", using_namespace);
            out.new_line();
        }
        if !self.config.csharp.namespace.is_empty() {
            write!(out, "namespace {};", self.config.csharp.namespace);
            out.new_line();
        }
    }

    fn close_namespaces<W: std::io::Write>(&mut self, _out: &mut SourceWriter<W>) {}

    fn write_headers<W: std::io::Write>(&self, out: &mut SourceWriter<W>, package_version: &str) {
        // Tells analyzers and IDEs that the file is generated.
        out.write("// <auto-generated />");
        out.new_line();
        // The nullable context is disabled in generated code unless explicitly enabled.
        out.write("#nullable enable");
        out.new_line();
        if self.config.package_version {
            write!(out, "// Package version: {package_version}");
            out.new_line();
        }
        if self.config.include_version {
            out.new_line_if_not_start();
            write!(
                out,
                "// Generated by cbindgen:{}",
                crate::bindgen::config::VERSION
            );
            out.new_line();
        }
        if let Some(ref f) = self.config.autogen_warning {
            out.new_line_if_not_start();
            write!(out, "{f}");
            out.new_line();
        }
        if let Some(ref header) = self.config.csharp.header {
            out.new_line_if_not_start();
            write!(out, "{header}");
        }
    }

    fn write_footers<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>) {
        _ = out;
    }

    fn write_enum<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, e: &Enum) {
        let has_data = e.tag.is_some();
        let condition = e.cfg.to_condition(self.config);
        condition.write_before(self.config, out);

        self.write_documentation(out, &e.documentation);
        if has_data {
            // The tag enum is nested in the data enum, so its values need no prefix.
            let mut e = e.clone();
            for variant in &mut e.variants {
                variant.export_name = variant.name.clone();
                reserved::escape_csharp(&mut variant.export_name);
            }
            self.write_data_enum(out, &e);
        } else {
            self.write_tag_enum(out, e);
        }

        condition.write_after(self.config, out);
    }

    fn write_struct<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, s: &Struct) {
        if s.annotations
            .bool("internal-derive-bitflags")
            .unwrap_or(false)
        {
            // TODO: Handle `usize` and `isize` bitflags, though they are
            // uncommon.
            self.write_bitflags_enum(out, s);
            return;
        }

        if s.fields.iter().any(|field| field.ty.contains_va_list()) {
            write!(
                out,
                "// cbindgen: Skipped '{}' because it contains va_list",
                s.export_name
            );
            return;
        }

        let condition = s.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        if !self.write_struct_attributes(
            out,
            "struct",
            &s.export_name,
            &s.alignment,
            &s.documentation,
            &s.annotations,
            "Sequential",
        ) {
            condition.write_after(self.config, out);
            return;
        }
        let has_pointers = s.fields.iter().any(|field| field.ty.is_ptr());
        let declaration = self.struct_declaration(has_pointers, true);
        write!(out, "{declaration} {}", s.export_name);
        let constants =
            Self::associated_constants(out.bindings(), &s.path, &s.associated_constants);
        if constants.is_empty() && s.fields.is_empty() {
            out.write(";");
            condition.write_after(self.config, out);
            return;
        }
        out.open_brace();
        for (i, constant) in constants.iter().enumerate() {
            if i > 0 {
                out.new_line();
                out.new_line();
            }
            self.write_constant(out, constant, Some(s));
        }
        if !constants.is_empty() && !s.fields.is_empty() {
            out.new_line();
            out.new_line();
        }
        out.write_vertical_source_list(
            self,
            &s.fields,
            ListType::Cap(""),
            Self::write_record_struct_field,
        );
        out.close_brace(false);

        condition.write_after(self.config, out);
    }

    fn write_union<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, u: &Union) {
        let condition = u.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        if !self.write_struct_attributes(
            out,
            "union",
            &u.export_name,
            &u.alignment,
            &u.documentation,
            &u.annotations,
            "Explicit",
        ) {
            condition.write_after(self.config, out);
            return;
        }
        let has_pointers = u.fields.iter().any(|field| field.ty.is_ptr());
        let declaration = self.struct_declaration(has_pointers, false);
        write!(out, "{declaration} {}", u.export_name);
        out.open_brace();
        out.write_vertical_source_list(
            self,
            &u.fields,
            ListType::Cap(""),
            Self::write_union_struct_field,
        );
        out.close_brace(false);

        condition.write_after(self.config, out);
    }

    fn write_opaque_item<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, o: &OpaqueItem) {
        let condition = o.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        self.write_documentation(out, &o.documentation);
        write!(out, "public readonly struct {}", o.export_name);
        out.open_brace();
        write!(out, "#pragma warning disable CS0169");
        out.new_line();
        write!(out, "private readonly byte _opaque;");
        out.new_line();
        write!(out, "#pragma warning restore CS0169");
        out.close_brace(false);
        condition.write_after(self.config, out);
    }

    fn write_type_def<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, t: &Typedef) {
        let condition = t.cfg.to_condition(self.config);
        condition.write_before(self.config, out);

        // C# type aliases cannot refer to other type aliases, so we keep a record of them and
        // resolve previous aliases.
        let aliased = match &t.aliased {
            Type::Path(path) => self.type_defs.get(path.export_name()).cloned(),
            _ => None,
        }
        .unwrap_or_else(|| t.aliased.clone());
        self.type_defs
            .insert(t.export_name.clone(), aliased.clone());
        if aliased != t.aliased {
            write!(out, "// ");
            self.write_type(out, &t.aliased);
            out.new_line();
        }
        write!(out, "using {} = ", t.export_name);
        self.write_type_argument(out, &aliased);
        out.write(";");
        condition.write_after(self.config, out);
    }

    fn write_static<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, s: &Static) {
        write!(
            out,
            "// cbindgen: Skipped '{}', because it is a static extern.",
            s.export_name
        );
    }

    fn write_type<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, t: &Type) {
        match t {
            Type::Ptr {
                ty,
                is_const: _,
                is_nullable: _,
                is_ref: _,
            } => {
                self.write_type(out, ty);
                out.write("*");
            }
            Type::Path(generic_path) => write!(out, "{}", generic_path.export_name()),
            Type::Primitive(primitive_type) => out.write(primitive_type.to_repr_csharp()),
            Type::Array(ty, ConstExpr::Value(size)) => {
                if let Ok(size) = size.parse::<usize>() {
                    if size == 0 {
                        panic!("Inline array size must be greater than zero");
                    } else if size == 1 {
                        // Arrays of size 1 have the same layout as a single element.
                        self.write_type(out, ty);
                    } else if size <= 16 {
                        // Use one of the built-in `InlineArrayN<T>` types.
                        write!(out, "System.Runtime.CompilerServices.InlineArray{size}<");
                        self.write_type_argument(out, ty);
                        out.write(">");
                    } else {
                        // Make our own inline array type.
                        self.inline_arrays
                            .entry((**ty).clone())
                            .or_default()
                            .insert(size);
                        write!(out, "InlineArray{size}_{}", self.get_mangled_type_name(ty));
                    }
                } else {
                    panic!("invalid array size")
                }
            }
            Type::Array(ty, ConstExpr::Path(path)) => {
                // Make our own inline array type.
                self.inline_arrays_constexpr
                    .entry((**ty).clone())
                    .or_default()
                    .insert(path.clone());
                write!(
                    out,
                    "InlineArray_{}_{}",
                    path.export_name(),
                    self.get_mangled_type_name(ty)
                );
            }
            Type::FuncPtr {
                ret,
                args,
                is_nullable: _,
                never_return: _,
            } => {
                out.write("delegate* unmanaged[Cdecl]<");
                for (i, arg) in args.iter().enumerate() {
                    if i > 0 {
                        out.write(", ");
                    }
                    self.write_type(out, &arg.1);
                    // Function pointer parameters cannot be named in C#, so keep the Rust names
                    // as comments. Names synthesized for `_` wildcards carry no information.
                    if let Some(name) = &arg.0 {
                        let is_wildcard = name
                            .strip_prefix('_')
                            .is_some_and(|rest| rest.chars().all(|ch| ch.is_ascii_digit()));
                        if !is_wildcard {
                            write!(out, " /* {name} */");
                        }
                    }
                }
                if !args.is_empty() {
                    out.write(", ");
                }
                self.write_type(out, ret);
                out.write(">");
            }
        }
    }

    fn write_documentation<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        d: &Documentation,
    ) {
        if d.doc_comment.is_empty() || !self.config.documentation {
            return;
        }
        let end = match self.config.documentation_length {
            DocumentationLength::Short => 1,
            DocumentationLength::Full => d.doc_comment.len(),
        };

        out.write("/// <summary>");
        out.new_line();
        for line in &d.doc_comment[..end] {
            // Documentation comments are XML, so escape the Markdown of the Rust documentation.
            let line = line
                .trim_end()
                .replace('&', "&amp;")
                .replace('<', "&lt;")
                .replace('>', "&gt;");
            if line.is_empty() || line.starts_with(' ') {
                write!(out, "///{line}");
            } else {
                write!(out, "/// {line}");
            }
            out.new_line();
        }
        out.write("/// </summary>");
        out.new_line();
    }

    fn write_literal<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, l: &Literal) {
        match l {
            Literal::Expr(v) => {
                // Handle UTF-32 character literals, which don't exist in C#.
                if v.starts_with("U'\\U") && v.ends_with('\'') {
                    let hex = &v[4..v.len() - 1];
                    if u32::from_str_radix(hex, 16).is_ok() {
                        write!(out, "0x{hex}");
                        return;
                    }
                }

                // Handle "ull" integer suffix, which don't exist in C#.
                if v.starts_with(|ch: char| ch.is_numeric() || ch == '-') && v.ends_with("ull") {
                    let value = &v[..v.len() - 3];
                    write!(out, "{value}");
                    return;
                }

                write!(out, "{v}")
            }
            Literal::Path {
                associated_to,
                name,
            } => {
                if let Some((path, export_name)) = associated_to {
                    if let Some(known) = to_known_assoc_constant(path, name) {
                        write!(out, "{known}");
                        return;
                    }
                    let text = match self.enums.get(path) {
                        Some(e) => match e.variants.iter().find(|v| v.name == *name) {
                            // A unit variant of a data enum.
                            Some(variant) if e.tag.is_some() => format!(
                                "new {export_name}(new {export_name}.{}())",
                                Self::variant_type_name(e, variant)
                            ),
                            // Enum variants may be renamed, so refer to them by their export name.
                            Some(variant) => format!("{export_name}.{}", variant.export_name),
                            // C# enums cannot have members, so their associated constants are
                            // members of the static class.
                            None if e.tag.is_none() => format!("{}.{name}", self.class_name),
                            None => format!(
                                "{export_name}.{}",
                                self.associated_constant_name(path, export_name, name)
                            ),
                        },
                        None => format!(
                            "{export_name}.{}",
                            associated_member_name(export_name, name)
                        ),
                    };
                    write!(out, "{text}");
                } else {
                    write!(out, "{}.{name}", self.class_name)
                }
            }
            Literal::PostfixUnaryOp { op, value } => {
                write!(out, "{op}");
                self.write_literal(out, value);
            }
            Literal::BinOp { left, op, right } => {
                out.write("(");
                self.write_literal(out, left);
                write!(out, " {op} ");
                // Shift operators in .NET take int as the right hand side argument, so insert a cast.
                if let &"<<" | &">>" = op {
                    out.write("(int)")
                }
                self.write_literal(out, right);
                out.write(")");
            }
            Literal::FieldAccess { base, field } => {
                // "Re-sugar" bitflags, which look like `(Flags.THE_FLAG).bits` in the IR.
                if let Literal::Path {
                    associated_to: Some((path, export_name)),
                    name,
                } = &**base
                {
                    if out.bindings().struct_is_bitflags(path) {
                        write!(
                            out,
                            "{export_name}.{}",
                            associated_member_name(export_name, name)
                        );
                        return;
                    }
                }

                out.write("(");
                self.write_literal(out, base);
                write!(out, ").{field}");
            }
            Literal::Struct {
                path,
                export_name,
                fields,
            } => {
                if out.bindings().struct_is_bitflags(path) {
                    // "Re-sugar" manual bitflags construction. This code path is basically
                    // only reached in the definitions of the "associated consts" (enum flag
                    // values) of bitflags.
                    let value = &fields.values().next().unwrap().value;
                    self.write_literal(out, value);
                    return;
                }

                // We don't actually need to reorder the fields, but `fields` is a `HashMap`, and we
                // do want deterministic ordering.
                let members: Vec<_> = out
                    .bindings()
                    .struct_field_names(path)
                    .iter()
                    .map(|(name, ty)| (name.clone(), name.clone(), ty.clone()))
                    .collect();
                self.write_object_creation(out, export_name, &members, fields);
            }
            Literal::EnumVariant {
                path,
                export_name,
                variant,
                fields,
            } => {
                // Validated by `Literal::is_valid`.
                let e = self.enums.get(path).unwrap();
                let variant = e.variants.iter().find(|v| v.name == *variant).unwrap();
                let type_name = format!("{export_name}.{}", Self::variant_type_name(e, variant));
                // Tuple fields are named `_0`, `_1`, etc. in literals, but not in the IR, and the
                // names in literals may have been renamed, so they are matched by index.
                let is_tuple = matches!(variant.body, VariantBody::Body { is_tuple: true, .. });
                let fields: HashMap<String, LiteralStructField> = if is_tuple {
                    fields
                        .iter()
                        .map(|(key, field)| {
                            let index = key.trim_start_matches(|ch: char| !ch.is_ascii_digit());
                            (format!("_{index}"), field.clone())
                        })
                        .collect()
                } else {
                    fields.clone()
                };
                let members: Vec<_> = Self::ir_variant_fields(e, variant)
                    .iter()
                    .zip(Self::variant_fields(e, variant))
                    .enumerate()
                    .map(|(i, (ir_field, field))| {
                        let key = if is_tuple {
                            format!("_{i}")
                        } else {
                            ir_field.name.clone()
                        };
                        (key, field.name, field.ty)
                    })
                    .collect();
                write!(out, "new {export_name}(");
                self.write_object_creation(out, &type_name, &members, &fields);
                out.write(")");
            }
            Literal::Cast { ty, value } => {
                out.write("unchecked((");
                self.write_type(out, ty);
                out.write(")");
                match (ty, &**value) {
                    // Bools do not coerce to integers in C#.
                    (Type::Primitive(PrimitiveType::Integer { .. }), Literal::Expr(value))
                        if value == "true" || value == "false" =>
                    {
                        out.write(if value == "true" { "1" } else { "0" });
                    }
                    _ => self.write_literal(out, value),
                };
                out.write(")");
            }
            Literal::Array { items } => {
                out.write("[");
                for (i, item) in items.iter().enumerate() {
                    if i > 0 {
                        out.write(", ");
                    }
                    self.write_literal(out, item);
                }
                out.write("]");
            }
        }
    }

    fn write_function_with_layout<W: std::io::Write>(
        &mut self,
        config: &Config,
        out: &mut SourceWriter<W>,
        func: &Function,
        layout: Layout,
    ) {
        let condition = func.cfg.to_condition(config);
        condition.write_before(config, out);

        self.write_documentation(out, &func.documentation);

        fn write_space<W: std::io::Write>(layout: Layout, out: &mut SourceWriter<W>) {
            if layout == Layout::Vertical {
                out.new_line();
            } else {
                out.write(" ")
            }
        }

        write!(
            out,
            "[LibraryImport({}, EntryPoint = \"{}\")]",
            self.import_library_expr,
            func.path()
        );
        out.new_line();
        self.write_obsolete_attribute(out, &func.annotations, DeprecatedNoteKind::Function);
        if func.never_return {
            out.write("[System.Diagnostics.CodeAnalysis.DoesNotReturn]");
            out.new_line();
        }
        // TODO: Apply `[MustUseReturnValue]`, but it exists in `JetBrains.Annotations`, so provide a
        // configuration option to enable it.
        let ret_marshalling = self.marshalling(out, &func.ret);
        if let Some(marshalling) = &ret_marshalling {
            write!(
                out,
                "[return: MarshalUsing(typeof({}))]",
                marshalling.marshaller
            );
            out.new_line();
        } else if let Type::Primitive(PrimitiveType::Bool) = func.ret {
            out.write("[return: MarshalAs(UnmanagedType.U1)]");
            out.new_line();
        }

        // Under the updated memory safety rules, extern methods must be marked either `safe` or
        // `unsafe`, which we take from the safety of the Rust function.
        if self.updated_memory_safety_rules() && !func.is_unsafe {
            out.write("public safe static partial ");
        } else {
            out.write("public unsafe static partial ");
        }
        match &ret_marshalling {
            Some(marshalling) => write!(out, "{}", marshalling.managed),
            None => self.write_bool_type(out, &func.ret),
        }
        write_space(layout, out);
        write!(out, "{}(", func.path());
        out.push_tab();
        for (i, arg) in func.args.iter().enumerate() {
            if i > 0 {
                write!(out, ",");
                write_space(layout, out);
            }
            if let Some(marshalling) = self.marshalling(out, &arg.ty) {
                write!(
                    out,
                    "[MarshalUsing(typeof({}))] {}",
                    marshalling.marshaller, marshalling.managed
                );
            } else if let Type::Primitive(PrimitiveType::Bool) = arg.ty {
                out.write("[MarshalAs(UnmanagedType.U1)] bool");
            } else {
                self.write_type(out, &arg.ty);
            }
            if let Some(arg_name) = arg.name.as_deref() {
                write!(out, " {arg_name}");
            } else {
                write!(out, " arg{i}");
            }
        }
        write!(out, ")");
        out.pop_tab();

        out.write(";");
        condition.write_after(config, out);
    }

    fn write_bindings<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, b: &Bindings) {
        // Note: The order is different from the other languages, because
        // imported functions and constants are inside the class, while
        // types are outside the class.
        for item in &b.items {
            if let ItemContainer::Enum(e) = item {
                self.enums
                    .entry(e.path.clone())
                    .or_insert_with(|| e.clone());
            }
        }
        self.write_headers(out, &b.package_version);
        self.open_namespaces(out);
        self.write_type_defs(out, b);
        self.write_items(out, b);
        self.write_primitive_constants(out, b);
        self.write_non_primitive_constants(out, b);
        self.write_enum_global_aliases(out, b);
        self.write_globals(out, b);
        self.write_functions(out, b);
        self.close_class(out);
        self.write_inline_arrays(out);
        self.close_namespaces(out);
        self.write_footers(out);
        self.write_trailer(out, b);
    }

    fn write_primitive_constants<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        b: &Bindings,
    ) {
        for constant in &b.constants {
            if constant.uses_only_primitive_types() && !self.is_member_constant(b, constant) {
                self.begin_class_member(out);
                constant.write(&b.config, self, out, None);
            }
        }
    }

    fn write_struct_or_typedef<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        s: &Struct,
        _b: &Bindings,
    ) {
        self.write_struct(out, s);
    }

    fn write_items<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, b: &Bindings) {
        for item in &b.items {
            if !item.deref().annotations().should_export() {
                continue;
            }

            // Typedefs are written at the top of the output.
            if let ItemContainer::Typedef(_) = item {
                continue;
            }

            out.new_line_if_not_start();
            match *item {
                ItemContainer::Constant(..) => unreachable!(),
                ItemContainer::Static(..) => unreachable!(),
                ItemContainer::Enum(ref x) => self.write_enum(out, x),
                ItemContainer::Struct(ref x) => self.write_struct_or_typedef(out, x, b),
                ItemContainer::Union(ref x) => self.write_union(out, x),
                ItemContainer::OpaqueItem(ref x) => self.write_opaque_item(out, x),
                ItemContainer::Typedef(..) => unreachable!(),
            }
            out.new_line();
        }
    }

    fn write_non_primitive_constants<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        b: &Bindings,
    ) {
        for constant in &b.constants {
            if !constant.uses_only_primitive_types() && !self.is_member_constant(b, constant) {
                self.begin_class_member(out);
                self.write_constant(out, constant, None);
            }
        }
    }

    fn write_globals<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, b: &Bindings) {
        self.write_globals_default(out, b)
    }

    fn write_globals_default<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        b: &Bindings,
    ) {
        for global in &b.globals {
            if !global.annotations.should_export() {
                continue;
            }
            self.begin_class_member(out);
            self.write_static(out, global);
        }
    }

    fn write_functions<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, b: &Bindings) {
        self.write_functions_default(out, b)
    }

    fn write_functions_default<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        b: &Bindings,
    ) {
        for function in &b.functions {
            if !function.annotations.should_export() {
                continue;
            }
            self.begin_class_member(out);
            if function.args.iter().any(|arg| arg.ty.contains_va_list()) {
                write!(
                    out,
                    "// cbindgen: Skipped '{}' because it uses va_list",
                    function.path.name()
                );
                continue;
            }
            self.write_function(&b.config, out, function);
        }
    }

    fn write_trailer<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, b: &Bindings) {
        // Note: The common trailer is not written, because tests write C code there containing
        // static asserts, but there's no easy way to convert that to valid C#.
        if let Some(ref trailer) = b.config.csharp.trailer {
            out.new_line_if_not_start();
            write!(out, "{trailer}");
            if !trailer.ends_with('\n') {
                out.new_line();
            }
        }
    }
}

/// The names of the fixed members of data enums.
const DATA_ENUM_MEMBERS: &[&str] = &[
    "Discriminant",
    "Value",
    "HasValue",
    "TryGetValue",
    "Equals",
    "GetHashCode",
    "ToString",
];

/// Parses a key of `CSharpConfig::marshallers`, which is either a type name, or a generic type
/// name with type parameters, e.g. `FfiSlice<T>`, into the name and the type parameters.
fn parse_marshaller_key(key: &str) -> Option<(&str, Vec<&str>)> {
    fn is_identifier(text: &str) -> bool {
        text.starts_with(|ch: char| ch.is_alphabetic() || ch == '_')
            && text.chars().all(|ch| ch.is_alphanumeric() || ch == '_')
    }

    let (name, params) = match key.split_once('<') {
        Some((name, rest)) => {
            let params = rest.strip_suffix('>')?.split(',').map(str::trim).collect();
            (name.trim(), params)
        }
        None => (key.trim(), Vec::new()),
    };
    (is_identifier(name) && params.iter().all(|param| is_identifier(param)))
        .then_some((name, params))
}

/// Replaces the identifiers in `text` that have a substitution, leaving everything else as is.
fn substitute_identifiers(text: &str, substitutions: &HashMap<&str, String>) -> String {
    let mut result = String::with_capacity(text.len());
    let mut rest = text;
    while let Some(start) = rest.find(|ch: char| ch.is_alphanumeric() || ch == '_') {
        result.push_str(&rest[..start]);
        rest = &rest[start..];
        let end = rest
            .find(|ch: char| !(ch.is_alphanumeric() || ch == '_'))
            .unwrap_or(rest.len());
        let identifier = &rest[..end];
        result.push_str(
            substitutions
                .get(identifier)
                .map_or(identifier, String::as_str),
        );
        rest = &rest[end..];
    }
    result.push_str(rest);
    result
}

/// The C# name of an associated constant `name` of the type `owner`, which is PascalCased
/// according to the C# naming conventions for members, e.g. `FLEX_START` becomes `FlexStart`.
fn associated_member_name(owner: &str, name: &str) -> String {
    let mut pascal = name.to_upper_camel_case();
    // Names like `_0` have no letters to convert.
    if !pascal.starts_with(|ch: char| ch.is_alphabetic()) {
        pascal = name.to_owned();
    }
    // Members cannot have the same name as their enclosing type.
    if pascal == owner {
        pascal.push('_');
    }
    pascal
}

/// Decodes a C string literal, as produced for `&str` and `&CStr` constants, back into its
/// bytes. Returns `None` if `lit` is not a string literal.
///
/// The IR only produces the escapes `\"`, `\\`, `\?`, `\n`, `\r`, `\t` and `\xNN`, and splits
/// the literal into adjacent literals (`"\xc3" "a"`) where a hex escape would otherwise
/// absorb the following character.
fn decode_c_string_literal(lit: &str) -> Option<Vec<u8>> {
    let mut bytes = Vec::new();
    let mut rest = lit.as_bytes();
    loop {
        rest = rest.strip_prefix(b"\"")?;
        loop {
            match *rest {
                [b'"', ref tail @ ..] => {
                    rest = tail;
                    break;
                }
                [b'\\', esc, ref tail @ ..] => {
                    rest = tail;
                    bytes.push(match esc {
                        b'"' | b'\\' | b'?' => esc,
                        b'n' => b'\n',
                        b'r' => b'\r',
                        b't' => b'\t',
                        b'x' => {
                            let hex = std::str::from_utf8(rest.get(..2)?).ok()?;
                            rest = &rest[2..];
                            u8::from_str_radix(hex, 16).ok()?
                        }
                        _ => return None,
                    });
                }
                [byte, ref tail @ ..] => {
                    rest = tail;
                    bytes.push(byte);
                }
                [] => return None,
            }
        }
        while let [b' ', ref tail @ ..] = *rest {
            rest = tail;
        }
        if rest.is_empty() {
            return Some(bytes);
        }
    }
}

/// Formats `s` as a C# regular string literal.
fn to_csharp_string_literal(s: &str) -> String {
    use std::fmt::Write as _;

    let mut out = String::with_capacity(s.len() + 2);
    out.push('"');
    for ch in s.chars() {
        match ch {
            '"' => out.push_str("\\\""),
            '\\' => out.push_str("\\\\"),
            '\0' => out.push_str("\\0"),
            '\n' => out.push_str("\\n"),
            '\r' => out.push_str("\\r"),
            '\t' => out.push_str("\\t"),
            ' ' | '!'..='~' => out.push(ch),
            // C#'s `\x` escape is variable-length, so use the fixed-length `\u` and `\U`.
            ch if (ch as u32) <= 0xFFFF => _ = write!(out, "\\u{:04X}", ch as u32),
            ch => _ = write!(out, "\\U{:08X}", ch as u32),
        }
    }
    out.push('"');
    out
}

fn to_known_assoc_constant(associated_to: &Path, name: &str) -> Option<String> {
    let name = match name {
        "MAX" => "MaxValue",
        "MIN" => "MinValue",
        _ => return None,
    };
    match PrimitiveType::maybe(associated_to.name())? {
        prim @ PrimitiveType::Integer {
            kind: IntKind::B8 | IntKind::B16 | IntKind::B32 | IntKind::B64,
            ..
        } => Some(format!("{}.{name}", prim.to_repr_csharp())),
        _ => None,
    }
}
