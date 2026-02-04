using System.Runtime.InteropServices;

public enum A : ulong
{
  a1 = 0,
  a2 = 2,
  a3,
  a4 = 5,
};

public enum B : uint
{
  b1 = 0,
  b2 = 2,
  b3,
  b4 = 5,
};

public enum C : ushort
{
  c1 = 0,
  c2 = 2,
  c3,
  c4 = 5,
};

public enum D : byte
{
  d1 = 0,
  d2 = 2,
  d3,
  d4 = 5,
};

[StructLayout(LayoutKind.Sequential)]
public readonly record struct E(nuint RawValue)
{
  public static readonly E e1 = new(0);
  public static readonly E e2 = new(2);
  public static readonly E e3 = new(e2.RawValue + 1);
  public static readonly E e4 = new(5);

}

[StructLayout(LayoutKind.Sequential)]
public readonly record struct F(nint RawValue)
{
  public static readonly F f1 = new(0);
  public static readonly F f2 = new(2);
  public static readonly F f3 = new(f2.RawValue + 1);
  public static readonly F f4 = new(5);

}

public enum L
{
  l1,
  l2,
  l3,
  l4,
};

public enum M : sbyte
{
  m1 = -1,
  m2 = 0,
  m3 = 1,
};

public enum N
{
  n1,
  n2,
  n3,
  n4,
};

public enum O : sbyte
{
  o1,
  o2,
  o3,
  o4,
};

public readonly struct J
{
  private readonly byte _opaque;
}

public readonly struct K
{
  private readonly byte _opaque;
}

public readonly struct Opaque
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Explicit)]
public struct G
{
  public enum G_Tag : byte
  {
    Foo,
    Bar,
    Baz,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct Foo_Body()
  {
    private readonly G_Tag _tag = G_Tag.Foo;
    public required short foo;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar_Body()
  {
    private readonly G_Tag _tag = G_Tag.Bar;
    public required byte x;
    public required short y;
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct H
{
  public enum H_Tag
  {
    H_Foo,
    H_Bar,
    H_Baz,
  };
  private H_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

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

}

[StructLayout(LayoutKind.Sequential)]
public struct ExI
{
  public enum ExI_Tag : byte
  {
    ExI_Foo,
    ExI_Bar,
    ExI_Baz,
  };
  private ExI_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public record struct ExI_Foo_Body()
    {
      public required short foo;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct ExI_Bar_Body()
    {
      public required byte x;
      public required short y;
    }

  }

}

[StructLayout(LayoutKind.Sequential)]
public struct P
{
  public enum P_Tag : byte
  {
    P0,
    P1,
  };
  private P_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public record struct P0_Body()
    {
      public required byte p0;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct P1_Body()
    {
      public required byte _0;
      public required byte _1;
      public required byte _2;
    }
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct Q
{
  public enum Q_Tag
  {
    Ok,
    Err,
  };
  private Q_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Ok_Body()
    {
      public required uint* ok;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct Err_Body()
    {
      public required uint err;
    }
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct R
{
  public enum R_Tag
  {
    IRFoo,
    IRBar,
    IRBaz,
  };
  private R_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public record struct IRFoo_Body()
    {
      public required short IRFoo;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct IRBar_Body()
    {
      public required byte x;
      public required short y;
    }

  }

}
public static partial class Api
{
  public const N n1 = N.n1;public const N n2 = N.n2;public const N n3 = N.n3;public const N n4 = N.n4;public const O o1 = O.o1;public const O o2 = O.o2;public const O o3 = O.o3;public const O o4 = O.o4;
  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(Opaque* opaque,
  A a,
  B b,
  C c,
  D d,
  E e,
  F f,
  G g,
  H h,
  ExI i,
  J j,
  K k,
  L l,
  M m,
  N n,
  O o,
  P p,
  Q q,
  R r);

}
