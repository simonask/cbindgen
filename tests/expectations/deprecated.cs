using System.Runtime.InteropServices;

[Obsolete("DEPRECATED_ENUM")]
public enum DeprecatedEnum : int
{
  A = 0,
};

[Obsolete("DEPRECATED_ENUM_WITH_NOTE(\"This is a note\")")]
public enum DeprecatedEnumWithNote : int
{
  B = 0,
};

public enum EnumWithDeprecatedVariants : int
{
  C = 0,
  [Obsolete("DEPRECATED_ENUM_VARIANT")]D = 1,
  [Obsolete("DEPRECATED_ENUM_VARIANT_WITH_NOTE(\"This is a note\")")]E = 2,
  [Obsolete("DEPRECATED_ENUM_VARIANT_WITH_NOTE(\"This is a note\")")]F = 3,
};

[Obsolete("DEPRECATED_STRUCT")][StructLayout(LayoutKind.Sequential)]
public  partial record struct DeprecatedStruct
{

  public required int a;
}

[Obsolete("DEPRECATED_STRUCT_WITH_NOTE(\"This is a note\")")][StructLayout(LayoutKind.Sequential)]
public  partial record struct DeprecatedStructWithNote
{

  public required int a;
}

[StructLayout(LayoutKind.Explicit)]
public struct EnumWithDeprecatedStructVariants
{
  public enum EnumWithDeprecatedStructVariants_Tag : byte
  {
    Foo,
    [Obsolete("DEPRECATED_ENUM_VARIANT")]Bar,
    [Obsolete("DEPRECATED_ENUM_VARIANT_WITH_NOTE(\"This is a note\")")]Baz,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct Foo_Body()
  {
    private readonly EnumWithDeprecatedStructVariants_Tag _tag = EnumWithDeprecatedStructVariants_Tag.Foo;
    public required short foo;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar_Body()
  {
    private readonly EnumWithDeprecatedStructVariants_Tag _tag = EnumWithDeprecatedStructVariants_Tag.Bar;
    public required byte x;
    public required short y;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Baz_Body()
  {
    private readonly EnumWithDeprecatedStructVariants_Tag _tag = EnumWithDeprecatedStructVariants_Tag.Baz;
    public required byte x;
    public required byte y;
  }
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "deprecated_without_note")]
  [Obsolete("DEPRECATED_FUNC")]
  public unsafe static partial void deprecated_without_note();

  [LibraryImport("library", EntryPoint = "deprecated_without_bracket")]
  [Obsolete("DEPRECATED_FUNC_WITH_NOTE(\"This is a note\")")]
  public unsafe static partial void deprecated_without_bracket();

  [LibraryImport("library", EntryPoint = "deprecated_with_note")]
  [Obsolete("DEPRECATED_FUNC_WITH_NOTE(\"This is a note\")")]
  public unsafe static partial void deprecated_with_note();

  [LibraryImport("library", EntryPoint = "deprecated_with_note_and_since")]
  [Obsolete("DEPRECATED_FUNC_WITH_NOTE(\"This is a note\")")]
  public unsafe static partial void deprecated_with_note_and_since();

  [LibraryImport("library", EntryPoint = "deprecated_with_note_which_requires_to_be_escaped")]
  [Obsolete("DEPRECATED_FUNC_WITH_NOTE(\"This quote \\\" requires to be quoted, and this [\\n] requires to be escaped\")")]
  public unsafe static partial void
  deprecated_with_note_which_requires_to_be_escaped();

  [LibraryImport("library", EntryPoint = "dummy")]
  public unsafe static partial void
  dummy(DeprecatedEnum a,
  DeprecatedEnumWithNote b,
  EnumWithDeprecatedVariants c,
  DeprecatedStruct d,
  DeprecatedStructWithNote e,
  EnumWithDeprecatedStructVariants f);

}
