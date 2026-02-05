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

  public static G Foo(short foo)
     => new(new Foo_Body
      {
        foo = foo,

      });
  public static G Bar(byte x, short y)
     => new(new Bar_Body
      {
        x = x,
        y = y,

      });
  public static G Baz() => new() { _tag = Tag.Baz };

  public G(Foo_Body foo)
  {
    this.foo = foo;
  }
  public G(Bar_Body bar)
  {
    this.bar = bar;
  }


  public static implicit operator G(Foo_Body value) => new(value);
  public static implicit operator G(Bar_Body value) => new(value);

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
  public enum Tag
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
public struct ExI
{
  public enum Tag : byte
  {
    ExI_Foo,
    ExI_Bar,
    ExI_Baz,
  };
  private Tag _tag;
  private ExI_Data _data;

  public static ExI Foo(short foo)
     => new(new ExI_Foo_Body
      {
        foo = foo,

      });
  public static ExI Bar(byte x, short y)
     => new(new ExI_Bar_Body
      {
        x = x,
        y = y,

      });
  public static ExI Baz() => new() { _tag = Tag.ExI_Baz };

  public ExI(ExI_Foo_Body foo)
  {
    _tag = Tag.ExI_Foo;
    _data.foo = foo;
  }
  public ExI(ExI_Bar_Body bar)
  {
    _tag = Tag.ExI_Bar;
    _data.bar = bar;
  }


  public static implicit operator ExI(ExI_Foo_Body value) => new(value);
  public static implicit operator ExI(ExI_Bar_Body value) => new(value);

  public readonly bool IsFoo => _tag == Tag.ExI_Foo;
  public readonly bool IsBar => _tag == Tag.ExI_Bar;
  public readonly bool IsBaz => _tag == Tag.ExI_Baz;

  public readonly ExI_Foo_Body? AsFoo => _tag == Tag.ExI_Foo ? _data.foo : null;
  public readonly ExI_Bar_Body? AsBar => _tag == Tag.ExI_Bar ? _data.bar : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct ExI_Data
  {
    [FieldOffset(0)]
    public ExI_Foo_Body foo;
    [FieldOffset(0)]
    public ExI_Bar_Body bar;
  }
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

[StructLayout(LayoutKind.Sequential)]
public struct P
{
  public enum Tag : byte
  {
    P0,
    P1,
  };
  private Tag _tag;
  private P_Data _data;

  public static P P0(byte p0)
     => new(new P0_Body
      {
        p0 = p0,

      });
  public static P P1(byte _0, byte _1, byte _2)
     => new(new P1_Body
      {
        _0 = _0,
        _1 = _1,
        _2 = _2,

      });

  public P(P0_Body p0)
  {
    _tag = Tag.P0;
    _data.p0 = p0;
  }
  public P(P1_Body p1)
  {
    _tag = Tag.P1;
    _data.p1 = p1;
  }

  public static implicit operator P(P0_Body value) => new(value);
  public static implicit operator P(P1_Body value) => new(value);

  public readonly bool IsP0 => _tag == Tag.P0;
  public readonly bool IsP1 => _tag == Tag.P1;

  public readonly P0_Body? AsP0 => _tag == Tag.P0 ? _data.p0 : null;
  public readonly P1_Body? AsP1 => _tag == Tag.P1 ? _data.p1 : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct P_Data
  {
    [FieldOffset(0)]
    public P0_Body p0;
    [FieldOffset(0)]
    public P1_Body p1;
  }
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

[StructLayout(LayoutKind.Sequential)]
public struct Q
{
  public enum Tag
  {
    Ok,
    Err,
  };
  private Tag _tag;
  private Q_Data _data;

  public unsafe static Q Ok(uint* ok)
     => new(new Ok_Body
      {
        ok = ok,

      });
  public static Q Err(uint err)
     => new(new Err_Body
      {
        err = err,

      });

  public Q(Ok_Body ok)
  {
    _tag = Tag.Ok;
    _data.ok = ok;
  }
  public Q(Err_Body err)
  {
    _tag = Tag.Err;
    _data.err = err;
  }

  public static implicit operator Q(Ok_Body value) => new(value);
  public static implicit operator Q(Err_Body value) => new(value);

  public readonly bool IsOk => _tag == Tag.Ok;
  public readonly bool IsErr => _tag == Tag.Err;

  public readonly Ok_Body? AsOk => _tag == Tag.Ok ? _data.ok : null;
  public readonly Err_Body? AsErr => _tag == Tag.Err ? _data.err : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct Q_Data
  {
    [FieldOffset(0)]
    public Ok_Body ok;
    [FieldOffset(0)]
    public Err_Body err;
  }
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

[StructLayout(LayoutKind.Sequential)]
public struct R
{
  public enum Tag
  {
    IRFoo,
    IRBar,
    IRBaz,
  };
  private Tag _tag;
  private R_Data _data;

  public static R IRFoo(short IRFoo)
     => new(new IRFoo_Body
      {
        IRFoo = IRFoo,

      });
  public static R IRBar(byte x, short y)
     => new(new IRBar_Body
      {
        x = x,
        y = y,

      });
  public static R IRBaz() => new() { _tag = Tag.IRBaz };

  public R(IRFoo_Body IRFoo)
  {
    _tag = Tag.IRFoo;
    _data.IRFoo = IRFoo;
  }
  public R(IRBar_Body IRBar)
  {
    _tag = Tag.IRBar;
    _data.IRBar = IRBar;
  }


  public static implicit operator R(IRFoo_Body value) => new(value);
  public static implicit operator R(IRBar_Body value) => new(value);

  public readonly bool IsIRFoo => _tag == Tag.IRFoo;
  public readonly bool IsIRBar => _tag == Tag.IRBar;
  public readonly bool IsIRBaz => _tag == Tag.IRBaz;

  public readonly IRFoo_Body? AsIRFoo => _tag == Tag.IRFoo ? _data.IRFoo : null;
  public readonly IRBar_Body? AsIRBar => _tag == Tag.IRBar ? _data.IRBar : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct R_Data
  {
    [FieldOffset(0)]
    public IRFoo_Body IRFoo;
    [FieldOffset(0)]
    public IRBar_Body IRBar;
  }
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
