using System.Runtime.InteropServices;

public enum MyCLikeEnum
{
  Foo1,
  Bar1,
  Baz1,
};

public enum MyCLikeEnum_Prepended
{
  Foo1_Prepended,
  Bar1_Prepended,
  Baz1_Prepended,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct MyFancyStruct
{

  public required int i;
}

[StructLayout(LayoutKind.Sequential)]
public struct MyFancyEnum
{
  public enum Tag
  {
    Foo,
    Bar,
    Baz,
  };
  private Tag _tag;
  private MyFancyEnum_Data _data;

  public static MyFancyEnum Foo() => new() { _tag = Tag.Foo };
  public static MyFancyEnum Bar(int bar)
     => new(new Bar_Body
      {
        bar = bar,

      });
  public static MyFancyEnum Baz(int baz)
     => new(new Baz_Body
      {
        baz = baz,

      });


  public MyFancyEnum(Bar_Body bar)
  {
    _tag = Tag.Bar;
    _data.bar = bar;
  }
  public MyFancyEnum(Baz_Body baz)
  {
    _tag = Tag.Baz;
    _data.baz = baz;
  }

  public static implicit operator MyFancyEnum(Bar_Body value) => new(value);
  public static implicit operator MyFancyEnum(Baz_Body value) => new(value);

  public readonly bool IsFoo => _tag == Tag.Foo;
  public readonly bool IsBar => _tag == Tag.Bar;
  public readonly bool IsBaz => _tag == Tag.Baz;

  public readonly Bar_Body? AsBar => _tag == Tag.Bar ? _data.bar : null;
  public readonly Baz_Body? AsBaz => _tag == Tag.Baz ? _data.baz : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct MyFancyEnum_Data
  {
    [FieldOffset(0)]
    public Bar_Body bar;
    [FieldOffset(0)]
    public Baz_Body baz;
  }

  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar_Body()
  {
    public required int bar;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Baz_Body()
  {
    public required int baz;
  }

}

[StructLayout(LayoutKind.Explicit)]
public  partial struct MyUnion
{
  [FieldOffset(0)]
  public float f;
  [FieldOffset(0)]
  public uint u;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct MyFancyStruct_Prepended
{

  public required int i;
}

[StructLayout(LayoutKind.Sequential)]
public struct MyFancyEnum_Prepended
{
  public enum Tag
  {
    Foo_Prepended,
    Bar_Prepended,
    Baz_Prepended,
  };
  private Tag _tag;
  private MyFancyEnum_Prepended_Data _data;

  public static MyFancyEnum_Prepended Foo_Prepended() => new() { _tag = Tag.Foo_Prepended };
  public static MyFancyEnum_Prepended Bar_Prepended(int bar_prepended)
     => new(new Bar_Prepended_Body
      {
        bar_prepended = bar_prepended,

      });
  public static MyFancyEnum_Prepended Baz_Prepended(int baz_prepended)
     => new(new Baz_Prepended_Body
      {
        baz_prepended = baz_prepended,

      });


  public MyFancyEnum_Prepended(Bar_Prepended_Body bar_prepended)
  {
    _tag = Tag.Bar_Prepended;
    _data.bar_prepended = bar_prepended;
  }
  public MyFancyEnum_Prepended(Baz_Prepended_Body baz_prepended)
  {
    _tag = Tag.Baz_Prepended;
    _data.baz_prepended = baz_prepended;
  }

  public static implicit operator MyFancyEnum_Prepended(Bar_Prepended_Body value) => new(value);
  public static implicit operator MyFancyEnum_Prepended(Baz_Prepended_Body value) => new(value);

  public readonly bool IsFoo_Prepended => _tag == Tag.Foo_Prepended;
  public readonly bool IsBar_Prepended => _tag == Tag.Bar_Prepended;
  public readonly bool IsBaz_Prepended => _tag == Tag.Baz_Prepended;

  public readonly Bar_Prepended_Body? AsBar_Prepended => _tag == Tag.Bar_Prepended ? _data.bar_prepended : null;
  public readonly Baz_Prepended_Body? AsBaz_Prepended => _tag == Tag.Baz_Prepended ? _data.baz_prepended : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct MyFancyEnum_Prepended_Data
  {
    [FieldOffset(0)]
    public Bar_Prepended_Body bar_prepended;
    [FieldOffset(0)]
    public Baz_Prepended_Body baz_prepended;
  }

  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar_Prepended_Body()
  {
    public required int bar_prepended;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Baz_Prepended_Body()
  {
    public required int baz_prepended;
  }

}

[StructLayout(LayoutKind.Explicit)]
public  partial struct MyUnion_Prepended
{
  [FieldOffset(0)]
  public float f;
  [FieldOffset(0)]
  public uint u;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(MyFancyStruct s,
    MyFancyEnum e,
    MyCLikeEnum c,
    MyUnion u,
    MyFancyStruct_Prepended sp,
    MyFancyEnum_Prepended ep,
    MyCLikeEnum_Prepended cp,
    MyUnion_Prepended up);

}
