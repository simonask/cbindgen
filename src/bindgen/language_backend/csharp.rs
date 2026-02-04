use std::{
    borrow::Cow,
    collections::{BTreeMap, BTreeSet},
};

use heck::ToUpperCamelCase;

use crate::{
    bindgen::{
        ir::{
            ConditionWrite as _, ConstExpr, Constant, Enum, EnumVariant, Field, IntKind, Item,
            ItemContainer, Literal, Path, PrimitiveType, ReprAlign, Struct, ToCondition, Type,
            Typedef, VariantBody,
        },
        language_backend::LanguageBackend,
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
    inline_arrays_constexpr: BTreeMap<Type, BTreeSet<String>>,
    /// Function pointer types to generate delegate types for.
    delegates: BTreeSet<Type>,
    type_defs: BTreeMap<String, Type>,
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

        CSharpLanguageBackend {
            config,
            class_name,
            import_library_expr,
            inline_arrays: BTreeMap::new(),
            inline_arrays_constexpr: BTreeMap::new(),
            delegates: BTreeSet::new(),
            type_defs: BTreeMap::new(),
        }
    }

    fn open_class<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>) {
        write!(out, "public static partial class {}", self.class_name);
        out.open_brace();
    }

    fn close_class<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>) {
        out.close_brace(false);
        out.new_line();
    }

    fn write_enum_variant<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        u: &EnumVariant,
    ) {
        let condition = u.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        self.write_documentation(out, &u.documentation);
        if let Some(note) = u.body.annotations().deprecated_note(
            self.config,
            crate::bindgen::ir::DeprecatedNoteKind::EnumVariant,
        ) {
            write!(out, "[Obsolete(\"{}\")]", note.escape_default());
        }
        write!(out, "{}", u.export_name);
        if let Some(discriminant) = &u.discriminant {
            out.write(" = ");
            self.write_literal(out, discriminant);
        }
        out.write(",");
        condition.write_after(self.config, out);
    }

    fn write_enum_data_variant<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        variant: &EnumVariant,
        tag_name: &str,
        inline_tag_field: bool,
    ) {
        let VariantBody::Body { ref body, .. } = variant.body else {
            return;
        };
        let condition = variant.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        self.write_documentation(out, &variant.documentation);
        out.write("[StructLayout(LayoutKind.Sequential)]");
        out.new_line();
        let has_pointers = body.fields.iter().any(|field| field.ty.is_ptr());
        let record = if has_pointers { "unsafe" } else { "record" };

        write!(out, "public {record} struct {}()", body.export_name);
        out.open_brace();
        if inline_tag_field {
            write!(
                out,
                "private readonly {tag_name} _tag = {tag_name}.{};",
                variant.export_name
            );
            out.new_line();
        }
        if let VariantBody::Body { ref body, .. } = variant.body {
            out.write_vertical_source_list(
                self,
                if inline_tag_field {
                    &body.fields[1..]
                } else {
                    &body.fields
                },
                ListType::Cap(""),
                Self::write_record_struct_field,
            );
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
        out.write("public ");
        self.write_type(out, &f.ty);
        write!(out, " {}", f.name);
        out.write(";");
        condition.write_after(self.config, out);
    }

    fn write_inline_arrays<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>) {
        for (ty, sizes) in std::mem::take(&mut self.inline_arrays) {
            let name = self.get_mangled_type_name(&ty);
            for size in sizes {
                if size <= 16 {
                    continue;
                }

                write!(out, "[System.Runtime.CompilerServices.InlineArray({size})]");
                out.new_line();
                write!(out, "public struct InlineArray{size}_{name}");
                out.open_brace();
                out.write("private ");
                if ty.is_ptr() {
                    // Pointers cannot be type arguments, so cannot be the target type in inline arrays.
                    out.write("nint");
                } else {
                    self.write_type(out, &ty);
                }
                out.write(" _data;");
                out.close_brace(false);
                out.new_line();
            }
        }

        for (ty, names) in std::mem::take(&mut self.inline_arrays_constexpr) {
            let name = self.get_mangled_type_name(&ty);
            for size_name in names {
                write!(
                    out,
                    "[System.Runtime.CompilerServices.InlineArray((int){}.{size_name})]",
                    self.class_name
                );
                out.new_line();
                write!(out, "public struct InlineArray_{size_name}_{name}");
                out.open_brace();
                out.write("private ");
                if ty.is_ptr() {
                    // Pointers cannot be type arguments, so cannot be the target type in inline arrays.
                    out.write("nint");
                } else {
                    self.write_type(out, &ty);
                }
                out.write(" _data;");
                out.close_brace(false);
                out.new_line();
            }
        }
    }

    fn write_delegates<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>) {
        // Loop to ensure we catch delegates that were discovered as part of writing
        // other delegates.
        while !self.delegates.is_empty() {
            for delegate in std::mem::take(&mut self.delegates) {
                let mangled_name = self.get_mangled_type_name(&delegate);
                let Type::FuncPtr {
                    ret,
                    args,
                    is_nullable: _,
                    never_return,
                } = delegate
                else {
                    continue;
                };
                out.write("[UnmanagedFunctionPointer(CallingConvention.Cdecl)]");
                out.new_line();
                if never_return {
                    out.write("[System.Diagnostics.CodeAnalysis.DoesNotReturn]");
                    out.new_line();
                } else if let Type::Primitive(PrimitiveType::Bool) = *ret {
                    out.write("[return: MarshalAs(UnmanagedType.I1)]");
                    out.new_line();
                }
                out.write("public delegate ");
                if let Type::Primitive(PrimitiveType::Bool) = *ret {
                    out.write("bool");
                } else {
                    self.write_type(out, &ret);
                }
                write!(out, " {mangled_name}(");
                for (i, arg) in args.iter().enumerate() {
                    if i > 0 {
                        out.write(", ");
                    }
                    if let Type::Primitive(PrimitiveType::Bool) = arg.1 {
                        out.write("[MarshalAs(UnmanagedType.I1)] bool");
                    } else {
                        self.write_type(out, &arg.1);
                    }
                    out.write(" ");
                    if let Some(ref arg_name) = arg.0 {
                        write!(out, "{arg_name}");
                    } else {
                        write!(out, "_arg{i}");
                    }
                }
                out.write(");");
                out.new_line();
            }
        }
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
                for variant in &e.variants {
                    self.write_documentation(out, &variant.documentation);
                    if let Some(ref discriminant) = variant.discriminant {
                        write!(
                            out,
                            "public static readonly {} {} = new(",
                            e.export_name, variant.export_name
                        );
                        self.write_literal(out, discriminant);
                        write!(out, ");");
                    } else if let Some(previous_variant) = previous_variant {
                        // TODO: This breaks down when the previous variant was conditionally
                        // compiled.
                        write!(
                            out,
                            "public static readonly {} {} = new({}.RawValue + 1);",
                            e.export_name, variant.export_name, previous_variant.export_name
                        );
                    } else {
                        // First variant, no discriminant, set to zero.
                        write!(
                            out,
                            "public static readonly {} {} = new(0);",
                            e.export_name, variant.export_name
                        );
                    }
                    out.new_line();
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
                self.write_type_def(out, &typedef);
                out.new_line();
            }
            // else if let ItemContainer::Struct(s) = item {
            //     if let Some(typedef) = s.as_typedef() {
            //         self.write_type_def(out, &typedef);
            //         out.new_line();
            //     }
            // }
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
                    let condition = enum_.cfg.to_condition(self.config);
                    condition.write_before(self.config, out);
                    for variant in &enum_.variants {
                        let condition = variant.cfg.to_condition(self.config);
                        condition.write_before(self.config, out);
                        write!(
                            out,
                            "public const {} {} = {}.{};",
                            enum_.export_name, variant.export_name, enum_.export_name, variant.name
                        );
                        condition.write_after(self.config, out);
                    }
                    condition.write_after(self.config, out);
                }
            }
        }
    }

    fn write_constant<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        constant: &Constant,
        associated_to: Option<&Struct>,
    ) {
        if constant.uses_only_primitive_types() {
            constant.write(self.config, self, out, associated_to);
        } else {
            if !constant.value.is_valid(out.bindings()) {
                return;
            }
            let condition = constant.cfg.to_condition(self.config);
            condition.write_before(self.config, out);
            if constant.value.has_pointer_casts() {
                write!(
                    out,
                    "// cbindgen: Skipped '{}' because it contains pointer casts",
                    constant.name()
                );
                out.new_line();
                return;
            }
            self.write_documentation(out, &constant.documentation);
            out.write("public static readonly ");
            self.write_type(out, &constant.ty);
            write!(out, " {} = ", constant.export_name);
            self.write_literal(out, &constant.value);
            write!(out, ";");
            condition.write_after(self.config, out);
            out.new_line();
        }
    }
}

impl LanguageBackend for CSharpLanguageBackend<'_> {
    fn open_namespaces<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>) {
        out.write("using System.Runtime.InteropServices;");
        out.new_line();
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

    fn write_enum<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        e: &crate::bindgen::ir::Enum,
    ) {
        let has_data = e.tag.is_some();
        let inline_tag_field = Enum::inline_tag_field(&e.repr);
        let tag_name = e.tag_name();

        let condition = e.cfg.to_condition(self.config);
        condition.write_before(self.config, out);

        self.write_documentation(out, &e.documentation);
        if has_data {
            if inline_tag_field {
                // Emit a struct with the tag for each variant.
                out.write("[StructLayout(LayoutKind.Explicit)]");
                out.new_line();
                write!(out, "public struct {}", e.export_name());
                out.open_brace();
                self.write_tag_enum(out, e);
                out.new_line();
                for variant in &e.variants {
                    out.new_line();
                    self.write_enum_data_variant(out, variant, tag_name, true);
                }
                out.close_brace(false);
            } else {
                // External tag in the containing struct.
                out.write("[StructLayout(LayoutKind.Sequential)]");
                out.new_line();
                write!(out, "public struct {}", e.export_name());
                out.open_brace();
                self.write_tag_enum(out, e);
                out.new_line();
                write!(out, "private {tag_name} _tag;");
                out.new_line();
                write!(out, "private EnumData _data;");
                out.new_line();
                out.write("[StructLayout(LayoutKind.Explicit)]");
                out.new_line();
                write!(out, "private struct EnumData");
                out.open_brace();
                for variant in &e.variants {
                    out.new_line();
                    self.write_enum_data_variant(out, variant, tag_name, false);
                }
                out.close_brace(false); // EnumData
                out.new_line();
                out.close_brace(false); // Enum
            }
        } else {
            self.write_tag_enum(out, e);
        }

        condition.write_after(self.config, out);
    }

    fn write_struct<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        s: &crate::bindgen::ir::Struct,
    ) {
        if s.fields.iter().any(|field| field.ty.contains_va_list()) {
            write!(
                out,
                "// cbindgen: Skipped '{}' because it contains va_list",
                s.export_name
            );
            out.new_line();
            return;
        }

        let condition = s.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        let pack = match s.alignment {
            Some(ReprAlign::Packed) => true,
            Some(ReprAlign::Align(_)) => {
                write!(
                    out,
                    "// cbindgen: Ignoring struct {} because it is has alignment attributes",
                    s.export_name
                );
                return;
            }
            None => false,
        };
        self.write_documentation(out, &s.documentation);

        if let Some(note) = s
            .annotations
            .deprecated_note(self.config, crate::bindgen::ir::DeprecatedNoteKind::Struct)
        {
            write!(out, "[Obsolete(\"{}\")]", note.escape_default());
        }

        let has_pointers = s.fields.iter().any(|field| field.ty.is_ptr());
        let record = if has_pointers { "" } else { "record" };
        let unsaf = if has_pointers { "unsafe" } else { "" };

        write!(
            out,
            "[StructLayout(LayoutKind.Sequential{})]",
            if pack { ", Pack = 1" } else { "" }
        );
        out.new_line();
        write!(
            out,
            "public {unsaf} partial {record} struct {}",
            s.export_name
        );
        out.open_brace();
        for constant in &s.associated_constants {
            out.new_line_if_not_start();
            self.write_constant(out, constant, Some(s));
        }
        out.new_line_if_not_start();
        out.write_vertical_source_list(
            self,
            &s.fields,
            ListType::Cap(""),
            Self::write_record_struct_field,
        );
        out.close_brace(false);

        condition.write_after(self.config, out);
    }

    fn write_union<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        u: &crate::bindgen::ir::Union,
    ) {
        let condition = u.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        let pack = match u.alignment {
            Some(ReprAlign::Packed) => true,
            Some(ReprAlign::Align(n)) => {
                write!(
                    out,
                    "// cbindgen: Ignoring union {} because it is has alignment attributes",
                    u.export_name
                );
                return;
            }
            None => false,
        };
        self.write_documentation(out, &u.documentation);

        if let Some(note) = u
            .annotations
            .deprecated_note(self.config, crate::bindgen::ir::DeprecatedNoteKind::Struct)
        {
            write!(out, "[Obsolete(\"{}\")]", note.escape_default());
        }

        let has_pointers = u.fields.iter().any(|field| field.ty.is_ptr());
        let unsaf = if has_pointers { "unsafe" } else { "" };

        write!(
            out,
            "[StructLayout(LayoutKind.Explicit{})]",
            if pack { ", Pack = 1" } else { "" }
        );
        out.new_line();
        write!(out, "public {unsaf} partial struct {}", u.export_name);
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

    fn write_opaque_item<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        o: &crate::bindgen::ir::OpaqueItem,
    ) {
        let condition = o.cfg.to_condition(self.config);
        condition.write_before(self.config, out);
        self.write_documentation(out, &o.documentation);
        write!(out, "public readonly struct {}", o.export_name);
        out.open_brace();
        write!(out, "private readonly byte _opaque;");
        out.close_brace(false);
        condition.write_after(self.config, out);
    }

    fn write_type_def<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, t: &Typedef) {
        let condition = t.cfg.to_condition(self.config);
        condition.write_before(self.config, out);

        // C# type aliases cannot refer to other type aliases, so we keep a record of them and
        // resolve previous aliases.
        let aliased = if let Type::Path(ref path) = &t.aliased {
            if let Some(existing_alias) = self.type_defs.get(path.export_name()).cloned() {
                existing_alias.clone()
            } else {
                self.type_defs
                    .insert(t.export_name.clone(), t.aliased.clone());
                t.aliased.clone()
            }
        } else {
            self.type_defs
                .insert(t.export_name.clone(), t.aliased.clone());
            t.aliased.clone()
        };
        if aliased != t.aliased {
            write!(out, "// ");
            self.write_type(out, &t.aliased);
            out.new_line();
        }
        write!(out, "using {} = ", t.export_name);
        if aliased.is_ptr() {
            write!(out, "nint");
        } else {
            self.write_type(out, &aliased);
        }
        out.write(";");
        condition.write_after(self.config, out);
    }

    fn write_static<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        s: &crate::bindgen::ir::Static,
    ) {
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
                        if ty.is_ptr() {
                            out.write("nint");
                        } else {
                            self.write_type(out, ty);
                        }
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
            Type::Array(ty, ConstExpr::Name(name)) => {
                // Make our own inline array type.
                self.inline_arrays_constexpr
                    .entry((**ty).clone())
                    .or_default()
                    .insert(name.clone());
                write!(out, "InlineArray_{name}_{}", self.get_mangled_type_name(ty));
            }
            Type::FuncPtr {
                ret,
                args,
                is_nullable: _,
                never_return: _,
            } => {
                // self.delegates.insert(t.clone());
                // write!(out, "{}", self.get_mangled_type_name(t))
                out.write("delegate* unmanaged[Cdecl]<");
                for (i, arg) in args.iter().enumerate() {
                    if i > 0 {
                        out.write(", ");
                    }
                    self.write_type(out, &arg.1);
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
        d: &crate::bindgen::ir::Documentation,
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
            out.write("/// ");
            write!(out, "/// {line}");
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
                    if let Ok(_) = u32::from_str_radix(hex, 16) {
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
                    write!(out, "{export_name}.{name}");
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
                out.write("(");
                self.write_literal(out, base);
                write!(out, ").{field}");
            }
            Literal::Struct {
                path,
                export_name,
                fields,
            } => {
                write!(out, "new {export_name}");
                if fields.is_empty() {
                    out.write("()");
                } else {
                    out.open_brace();
                    // We don't actually need to reorder the fields, but `fields` is a `HashMap`, and we do want
                    // deterministic ordering.
                    let ordered_fields = out.bindings().struct_field_names(path);
                    for ordered_key in ordered_fields.iter() {
                        if let Some(lit) = fields.get(&ordered_key.0) {
                            let condition = lit.cfg.to_condition(self.config);
                            condition.write_before(self.config, out);
                            write!(out, "{} = (", ordered_key.0);
                            self.write_type(out, &ordered_key.1);
                            out.write(")");
                            self.write_literal(out, &lit.value);
                            out.write(",");
                            out.new_line();
                            condition.write_after(self.config, out);
                        }
                    }
                    out.close_brace(false);
                }
            }
            Literal::Cast { ty, value } => {
                out.write("(");
                self.write_type(out, ty);
                out.write(")");
                match (ty, &**value) {
                    // Bools do not coerce to integers in C#.
                    (Type::Primitive(PrimitiveType::Integer { .. }), Literal::Expr(ref value))
                        if value == "true" =>
                    {
                        out.write("1");
                    }
                    (Type::Primitive(PrimitiveType::Integer { .. }), Literal::Expr(ref value))
                        if value == "false" =>
                    {
                        out.write("0");
                    }
                    _ => self.write_literal(out, value),
                };
            }
        }
    }

    fn write_function_with_layout<W: std::io::Write>(
        &mut self,
        config: &Config,
        out: &mut SourceWriter<W>,
        func: &crate::bindgen::ir::Function,
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
        if let Some(note) = func
            .annotations
            .deprecated_note(config, crate::bindgen::ir::DeprecatedNoteKind::Function)
        {
            write!(out, "[Obsolete(\"{}\")]", note.escape_default());
            out.new_line();
        }
        if func.never_return {
            out.write("[System.Diagnostics.CodeAnalysis.DoesNotReturn]");
            out.new_line();
        }
        // TODO: Apply `[MustUseReturnValue]`, but it exists in `JetBrains.Annotations`, so provide a
        // configuration option to enable it.
        if let Type::Primitive(PrimitiveType::Bool) = func.ret {
            out.write("[return: MarshalAs(UnmanagedType.U1)]");
            out.new_line();
        }

        out.write("public unsafe static partial ");
        self.write_type(out, &func.ret);
        write_space(layout, out);
        write!(out, "{}(", func.path());
        for (i, arg) in func.args.iter().enumerate() {
            if i > 0 {
                write!(out, ",");
                write_space(layout, out);
            }
            self.write_type(out, &arg.ty);
            if let Some(arg_name) = arg.name.as_deref() {
                write!(out, " {arg_name}");
            } else {
                write!(out, " arg{i}");
            }
        }
        write!(out, ")");

        out.write(";");
        condition.write_after(config, out);
    }

    fn write_bindings<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, b: &Bindings) {
        // Note: The order is different from the other languages, because
        // imported functions and constants are inside the class, while
        // types are outside the class.
        self.write_headers(out, &b.package_version);
        self.open_namespaces(out);
        self.write_type_defs(out, b);
        self.write_items(out, b);
        self.open_class(out);
        self.write_primitive_constants(out, b);
        self.write_non_primitive_constants(out, b);
        self.write_enum_global_aliases(out, b);
        self.write_globals(out, b);
        self.write_functions(out, b);
        self.close_class(out);
        self.write_delegates(out);
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
            if constant.uses_only_primitive_types() {
                out.new_line_if_not_start();
                constant.write(&b.config, self, out, None);
                out.new_line();
            }
        }
    }

    fn write_struct_or_typedef<W: std::io::Write>(
        &mut self,
        out: &mut SourceWriter<W>,
        s: &crate::bindgen::ir::Struct,
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
            if !constant.uses_only_primitive_types() {
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
            out.new_line_if_not_start();
            self.write_static(out, global);
            out.new_line();
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
            if function.args.iter().any(|arg| arg.ty.contains_va_list()) {
                write!(
                    out,
                    "// cbindgen: Skipped '{}' because it uses va_list",
                    function.path.name()
                );
                out.new_line();
                continue;
            }

            out.new_line_if_not_start();
            self.write_function(&b.config, out, function);
            out.new_line();
        }
    }

    fn write_trailer<W: std::io::Write>(&mut self, out: &mut SourceWriter<W>, b: &Bindings) {
        // Note: Tests write C code here containing static asserts,
        // but there's no easy way to convert that to valid C#.
        _ = (out, b);
    }
}

fn to_known_assoc_constant(associated_to: &Path, name: &str) -> Option<String> {
    use crate::bindgen::ir::{IntKind, PrimitiveType};

    let name = match name {
        "MAX" => "MaxValue",
        "MIN" => "MinValue",
        _ => return None,
    };

    let prim = PrimitiveType::maybe(associated_to.name())?;
    let prefix = match prim {
        PrimitiveType::Integer {
            kind,
            signed,
            zeroable: _,
        } => match kind {
            IntKind::B8 => {
                if signed {
                    "sbyte"
                } else {
                    "byte"
                }
            }
            IntKind::B16 => {
                if signed {
                    "short"
                } else {
                    "ushort"
                }
            }
            IntKind::B32 => {
                if signed {
                    "int"
                } else {
                    "uint"
                }
            }
            IntKind::B64 => {
                if signed {
                    "long"
                } else {
                    "ulong"
                }
            }
            _ => return None,
        },
        _ => return None,
    };
    Some(format!("{prefix}.{name}"))
}
