using System.Runtime.InteropServices;

public readonly struct I
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public struct H
{
  public enum Tag : byte
  {
    H_Foo,
    H_Bar,
    H_Baz,
  };
  private Tag _tag;
  private H_Data _data;

  public static H Foo(short foo)
     => new(new H_Foo_Body
      {
        foo = foo,

      });
  public static H Bar(byte x, short y)
     => new(new H_Bar_Body
      {
        x = x,
        y = y,

      });
  public static H Baz() => new() { _tag = Tag.H_Baz };

  public H(H_Foo_Body foo)
  {
    _tag = Tag.H_Foo;
    _data.foo = foo;
  }
  public H(H_Bar_Body bar)
  {
    _tag = Tag.H_Bar;
    _data.bar = bar;
  }


  public static implicit operator H(H_Foo_Body value) => new(value);
  public static implicit operator H(H_Bar_Body value) => new(value);

  public readonly bool IsFoo => _tag == Tag.H_Foo;
  public readonly bool IsBar => _tag == Tag.H_Bar;
  public readonly bool IsBaz => _tag == Tag.H_Baz;

  public readonly H_Foo_Body? AsFoo => _tag == Tag.H_Foo ? _data.foo : null;
  public readonly H_Bar_Body? AsBar => _tag == Tag.H_Bar ? _data.bar : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct H_Data
  {
    [FieldOffset(0)]
    public H_Foo_Body foo;
    [FieldOffset(0)]
    public H_Bar_Body bar;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct H_Foo_Body()
  {
    public required short foo;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct H_Bar_Body()
  {
    public required byte x;
    public required short y;
  }


}

[StructLayout(LayoutKind.Sequential)]
public struct J
{
  public enum Tag : byte
  {
    J_Foo,
    J_Bar,
    J_Baz,
  };
  private Tag _tag;
  private J_Data _data;

  public static J Foo(short foo)
     => new(new J_Foo_Body
      {
        foo = foo,

      });
  public static J Bar(byte x, short y)
     => new(new J_Bar_Body
      {
        x = x,
        y = y,

      });
  public static J Baz() => new() { _tag = Tag.J_Baz };

  public J(J_Foo_Body foo)
  {
    _tag = Tag.J_Foo;
    _data.foo = foo;
  }
  public J(J_Bar_Body bar)
  {
    _tag = Tag.J_Bar;
    _data.bar = bar;
  }


  public static implicit operator J(J_Foo_Body value) => new(value);
  public static implicit operator J(J_Bar_Body value) => new(value);

  public readonly bool IsFoo => _tag == Tag.J_Foo;
  public readonly bool IsBar => _tag == Tag.J_Bar;
  public readonly bool IsBaz => _tag == Tag.J_Baz;

  public readonly J_Foo_Body? AsFoo => _tag == Tag.J_Foo ? _data.foo : null;
  public readonly J_Bar_Body? AsBar => _tag == Tag.J_Bar ? _data.bar : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct J_Data
  {
    [FieldOffset(0)]
    public J_Foo_Body foo;
    [FieldOffset(0)]
    public J_Bar_Body bar;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct J_Foo_Body()
  {
    public required short foo;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct J_Bar_Body()
  {
    public required byte x;
    public required short y;
  }


}

[StructLayout(LayoutKind.Explicit)]
public struct K
{
  public enum Tag : byte
  {
    K_Foo,
    K_Bar,
    K_Baz,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private K_Foo_Body foo;
  [FieldOffset(0)]
  private K_Bar_Body bar;

  public static K Foo(short foo)
     => new(new K_Foo_Body
      {
        foo = foo,

      });
  public static K Bar(byte x, short y)
     => new(new K_Bar_Body
      {
        x = x,
        y = y,

      });
  public static K Baz() => new() { _tag = Tag.K_Baz };

  public K(K_Foo_Body foo)
  {
    this.foo = foo;
  }
  public K(K_Bar_Body bar)
  {
    this.bar = bar;
  }


  public static implicit operator K(K_Foo_Body value) => new(value);
  public static implicit operator K(K_Bar_Body value) => new(value);

  public readonly bool IsFoo => _tag == Tag.K_Foo;
  public readonly bool IsBar => _tag == Tag.K_Bar;
  public readonly bool IsBaz => _tag == Tag.K_Baz;

  public readonly K_Foo_Body? AsFoo => _tag == Tag.K_Foo ? foo : null;
  public readonly K_Bar_Body? AsBar => _tag == Tag.K_Bar ? bar : null;


  [StructLayout(LayoutKind.Sequential)]
  public record struct K_Foo_Body()
  {
    private readonly Tag _tag = Tag.K_Foo;
    public required short foo;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct K_Bar_Body()
  {
    private readonly Tag _tag = Tag.K_Bar;
    public required byte x;
    public required short y;
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial void foo(H h, I i, J j, K k);

}
