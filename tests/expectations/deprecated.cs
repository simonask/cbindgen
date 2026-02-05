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
  public enum Tag : byte
  {
    Foo,
    [Obsolete("DEPRECATED_ENUM_VARIANT")]Bar,
    [Obsolete("DEPRECATED_ENUM_VARIANT_WITH_NOTE(\"This is a note\")")]Baz,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Foo_Body foo;
  [FieldOffset(0)]
  private Bar_Body bar;
  [FieldOffset(0)]
  private Baz_Body baz;

  public static EnumWithDeprecatedStructVariants Foo(short foo)
     => new(new Foo_Body
      {
        foo = foo,

      });
  public static EnumWithDeprecatedStructVariants Bar(byte x, short y)
     => new(new Bar_Body
      {
        x = x,
        y = y,

      });
  public static EnumWithDeprecatedStructVariants Baz(byte x, byte y)
     => new(new Baz_Body
      {
        x = x,
        y = y,

      });

  public EnumWithDeprecatedStructVariants(Foo_Body foo)
  {
    this.foo = foo;
  }
  public EnumWithDeprecatedStructVariants(Bar_Body bar)
  {
    this.bar = bar;
  }
  public EnumWithDeprecatedStructVariants(Baz_Body baz)
  {
    this.baz = baz;
  }

  public static implicit operator EnumWithDeprecatedStructVariants(Foo_Body value) => new(value);
  public static implicit operator EnumWithDeprecatedStructVariants(Bar_Body value) => new(value);
  public static implicit operator EnumWithDeprecatedStructVariants(Baz_Body value) => new(value);

  public readonly bool IsFoo => _tag == Tag.Foo;
  public readonly bool IsBar => _tag == Tag.Bar;
  public readonly bool IsBaz => _tag == Tag.Baz;

  public readonly Foo_Body? AsFoo => _tag == Tag.Foo ? foo : null;
  public readonly Bar_Body? AsBar => _tag == Tag.Bar ? bar : null;
  public readonly Baz_Body? AsBaz => _tag == Tag.Baz ? baz : null;


  [StructLayout(LayoutKind.Sequential)]
  public record struct Foo_Body()
  {
    private readonly Tag _tag = Tag.Foo;
    public required short foo;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar_Body()
  {
    private readonly Tag _tag = Tag.Bar;
    public required byte x;
    public required short y;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Baz_Body()
  {
    private readonly Tag _tag = Tag.Baz;
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
