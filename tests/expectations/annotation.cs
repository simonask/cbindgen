using System.Runtime.InteropServices;

public enum C : uint
{
  X = 2,
  Y,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct A
{

  public required int m0;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct B
{

  public required int x;
  public required float y;
}

[StructLayout(LayoutKind.Explicit)]
public struct F
{
  public enum Tag : byte
  {
    Foo,
    Bar,
    Baz,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Foo_Body foo;
  [FieldOffset(0)]
  private Bar_Body bar;

  public static F Foo(short foo)
     => new(new Foo_Body
      {
        foo = foo,

      });
  public static F Bar(byte x, short y)
     => new(new Bar_Body
      {
        x = x,
        y = y,

      });
  public static F Baz() => new() { _tag = Tag.Baz };

  public F(Foo_Body foo)
  {
    this.foo = foo;
  }
  public F(Bar_Body bar)
  {
    this.bar = bar;
  }


  public static implicit operator F(Foo_Body value) => new(value);
  public static implicit operator F(Bar_Body value) => new(value);

  public readonly bool IsFoo => _tag == Tag.Foo;
  public readonly bool IsBar => _tag == Tag.Bar;
  public readonly bool IsBaz => _tag == Tag.Baz;

  public readonly Foo_Body? AsFoo => _tag == Tag.Foo ? foo : null;
  public readonly Bar_Body? AsBar => _tag == Tag.Bar ? bar : null;


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

}

[StructLayout(LayoutKind.Sequential)]
public struct H
{
  public enum Tag : byte
  {
    Hello,
    There,
    Everyone,
  };
  private Tag _tag;
  private H_Data _data;

  public static H Hello(short hello)
     => new(new Hello_Body
      {
        hello = hello,

      });
  public static H There(byte x, short y)
     => new(new There_Body
      {
        x = x,
        y = y,

      });
  public static H Everyone() => new() { _tag = Tag.Everyone };

  public H(Hello_Body hello)
  {
    _tag = Tag.Hello;
    _data.hello = hello;
  }
  public H(There_Body there)
  {
    _tag = Tag.There;
    _data.there = there;
  }


  public static implicit operator H(Hello_Body value) => new(value);
  public static implicit operator H(There_Body value) => new(value);

  public readonly bool IsHello => _tag == Tag.Hello;
  public readonly bool IsThere => _tag == Tag.There;
  public readonly bool IsEveryone => _tag == Tag.Everyone;

  public readonly Hello_Body? AsHello => _tag == Tag.Hello ? _data.hello : null;
  public readonly There_Body? AsThere => _tag == Tag.There ? _data.there : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct H_Data
  {
    [FieldOffset(0)]
    public Hello_Body hello;
    [FieldOffset(0)]
    public There_Body there;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Hello_Body()
  {
    public required short hello;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct There_Body()
  {
    public required byte x;
    public required short y;
  }


}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A x, B y, C z, F f, H h);

}
