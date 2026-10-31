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
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct K
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct Opaque
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
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
  private Foo foo;
  [FieldOffset(0)]
  private Bar bar;

  public G(Foo foo)
  {
    this.foo = foo;
  }
  public G(Bar bar)
  {
    this.bar = bar;
  }
  public G(Baz _)
  {
    _tag = Tag.Baz;
  }

  public static implicit operator G(Foo value) => new(value);
  public static implicit operator G(Bar value) => new(value);
  public static implicit operator G(Baz value) => new(value);

  public readonly bool TryGetValue(out Foo value)
  {
    value = _tag == Tag.Foo ? foo : default;
    return _tag == Tag.Foo;
  }
  public readonly bool TryGetValue(out Bar value)
  {
    value = _tag == Tag.Bar ? bar : default;
    return _tag == Tag.Bar;
  }
  public readonly bool TryGetValue(out Baz value)
  {
    value = default;
    return _tag == Tag.Baz;
  }

  public override readonly bool Equals(object? obj) => obj is G other && Equals(other);
  public readonly bool Equals(G other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.Foo => foo.Equals(other.foo),
      Tag.Bar => bar.Equals(other.bar),
      _ => true,

    };

  }
  public static bool operator ==(G left, G right) => left.Equals(right);
  public static bool operator !=(G left, G right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    switch (_tag)
    {
      case Tag.Foo:
      {
        hashCode.Add(foo);
        break;
      }
      case Tag.Bar:
      {
        hashCode.Add(bar);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.Foo:
      {
        stringBuilder.Append(" { ");
        foo.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.Bar:
      {
        stringBuilder.Append(" { ");
        bar.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }


  [StructLayout(LayoutKind.Sequential)]
  public record struct Foo()
  {
    private readonly Tag _tag = Tag.Foo;
    public required short Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Foo(short value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar()
  {
    private readonly Tag _tag = Tag.Bar;
    public required byte x;
    public required short y;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Bar(byte x, short y) : this()
    {
      this.x = x;
      this.y = y;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  public record struct Baz();
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

  public H(Foo foo)
  {
    _tag = Tag.H_Foo;
    _data.foo = foo;
  }
  public H(Bar bar)
  {
    _tag = Tag.H_Bar;
    _data.bar = bar;
  }
  public H(Baz _)
  {
    _tag = Tag.H_Baz;
  }

  public static implicit operator H(Foo value) => new(value);
  public static implicit operator H(Bar value) => new(value);
  public static implicit operator H(Baz value) => new(value);

  public readonly bool TryGetValue(out Foo value)
  {
    value = _tag == Tag.H_Foo ? _data.foo : default;
    return _tag == Tag.H_Foo;
  }
  public readonly bool TryGetValue(out Bar value)
  {
    value = _tag == Tag.H_Bar ? _data.bar : default;
    return _tag == Tag.H_Bar;
  }
  public readonly bool TryGetValue(out Baz value)
  {
    value = default;
    return _tag == Tag.H_Baz;
  }

  public override readonly bool Equals(object? obj) => obj is H other && Equals(other);
  public readonly bool Equals(H other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.H_Foo => _data.foo.Equals(other._data.foo),
      Tag.H_Bar => _data.bar.Equals(other._data.bar),
      _ => true,

    };

  }
  public static bool operator ==(H left, H right) => left.Equals(right);
  public static bool operator !=(H left, H right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.H_Foo:
      {
        hashCode.Add(_data.foo);
        break;
      }
      case Tag.H_Bar:
      {
        hashCode.Add(_data.bar);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.H_Foo:
      {
        stringBuilder.Append(" { ");
        _data.foo.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.H_Bar:
      {
        stringBuilder.Append(" { ");
        _data.bar.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct H_Data
  {
    [FieldOffset(0)]
    public Foo foo;
    [FieldOffset(0)]
    public Bar bar;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Foo()
  {
    public required short Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Foo(short value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar()
  {
    public required byte x;
    public required short y;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Bar(byte x, short y) : this()
    {
      this.x = x;
      this.y = y;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  public record struct Baz();

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

  public ExI(Foo foo)
  {
    _tag = Tag.ExI_Foo;
    _data.foo = foo;
  }
  public ExI(Bar bar)
  {
    _tag = Tag.ExI_Bar;
    _data.bar = bar;
  }
  public ExI(Baz _)
  {
    _tag = Tag.ExI_Baz;
  }

  public static implicit operator ExI(Foo value) => new(value);
  public static implicit operator ExI(Bar value) => new(value);
  public static implicit operator ExI(Baz value) => new(value);

  public readonly bool TryGetValue(out Foo value)
  {
    value = _tag == Tag.ExI_Foo ? _data.foo : default;
    return _tag == Tag.ExI_Foo;
  }
  public readonly bool TryGetValue(out Bar value)
  {
    value = _tag == Tag.ExI_Bar ? _data.bar : default;
    return _tag == Tag.ExI_Bar;
  }
  public readonly bool TryGetValue(out Baz value)
  {
    value = default;
    return _tag == Tag.ExI_Baz;
  }

  public override readonly bool Equals(object? obj) => obj is ExI other && Equals(other);
  public readonly bool Equals(ExI other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.ExI_Foo => _data.foo.Equals(other._data.foo),
      Tag.ExI_Bar => _data.bar.Equals(other._data.bar),
      _ => true,

    };

  }
  public static bool operator ==(ExI left, ExI right) => left.Equals(right);
  public static bool operator !=(ExI left, ExI right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.ExI_Foo:
      {
        hashCode.Add(_data.foo);
        break;
      }
      case Tag.ExI_Bar:
      {
        hashCode.Add(_data.bar);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.ExI_Foo:
      {
        stringBuilder.Append(" { ");
        _data.foo.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.ExI_Bar:
      {
        stringBuilder.Append(" { ");
        _data.bar.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct ExI_Data
  {
    [FieldOffset(0)]
    public Foo foo;
    [FieldOffset(0)]
    public Bar bar;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Foo()
  {
    public required short Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Foo(short value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar()
  {
    public required byte x;
    public required short y;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Bar(byte x, short y) : this()
    {
      this.x = x;
      this.y = y;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  public record struct Baz();

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

  public P(P0 p0)
  {
    _tag = Tag.P0;
    _data.p0 = p0;
  }
  public P(P1 p1)
  {
    _tag = Tag.P1;
    _data.p1 = p1;
  }

  public static implicit operator P(P0 value) => new(value);
  public static implicit operator P(P1 value) => new(value);

  public readonly bool TryGetValue(out P0 value)
  {
    value = _tag == Tag.P0 ? _data.p0 : default;
    return _tag == Tag.P0;
  }
  public readonly bool TryGetValue(out P1 value)
  {
    value = _tag == Tag.P1 ? _data.p1 : default;
    return _tag == Tag.P1;
  }

  public override readonly bool Equals(object? obj) => obj is P other && Equals(other);
  public readonly bool Equals(P other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.P0 => _data.p0.Equals(other._data.p0),
      Tag.P1 => _data.p1.Equals(other._data.p1),
      _ => true,

    };

  }
  public static bool operator ==(P left, P right) => left.Equals(right);
  public static bool operator !=(P left, P right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.P0:
      {
        hashCode.Add(_data.p0);
        break;
      }
      case Tag.P1:
      {
        hashCode.Add(_data.p1);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.P0:
      {
        stringBuilder.Append(" { ");
        _data.p0.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.P1:
      {
        stringBuilder.Append(" { ");
        _data.p1.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct P_Data
  {
    [FieldOffset(0)]
    public P0 p0;
    [FieldOffset(0)]
    public P1 p1;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct P0()
  {
    public required byte Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public P0(byte value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct P1()
  {
    public required byte Item1;
    public required byte Item2;
    public required byte Item3;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public P1(byte item1, byte item2, byte item3) : this()
    {
      this.Item1 = item1;
      this.Item2 = item2;
      this.Item3 = item3;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
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

  public Q(Ok ok)
  {
    _tag = Tag.Ok;
    _data.ok = ok;
  }
  public Q(Err err)
  {
    _tag = Tag.Err;
    _data.err = err;
  }

  public static implicit operator Q(Ok value) => new(value);
  public static implicit operator Q(Err value) => new(value);

  public readonly bool TryGetValue(out Ok value)
  {
    value = _tag == Tag.Ok ? _data.ok : default;
    return _tag == Tag.Ok;
  }
  public readonly bool TryGetValue(out Err value)
  {
    value = _tag == Tag.Err ? _data.err : default;
    return _tag == Tag.Err;
  }

  public override readonly bool Equals(object? obj) => obj is Q other && Equals(other);
  public readonly bool Equals(Q other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.Ok => _data.ok.Equals(other._data.ok),
      Tag.Err => _data.err.Equals(other._data.err),
      _ => true,

    };

  }
  public static bool operator ==(Q left, Q right) => left.Equals(right);
  public static bool operator !=(Q left, Q right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.Ok:
      {
        hashCode.Add(_data.ok);
        break;
      }
      case Tag.Err:
      {
        hashCode.Add(_data.err);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.Ok:
      {
        stringBuilder.Append(" { ");
        _data.ok.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.Err:
      {
        stringBuilder.Append(" { ");
        _data.err.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct Q_Data
  {
    [FieldOffset(0)]
    public Ok ok;
    [FieldOffset(0)]
    public Err err;
  }
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct Ok()
  {
    public required uint* Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Ok(uint* value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => stringBuilder.Append("...");
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Err()
  {
    public required uint Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Err(uint value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
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

  public R(IRFoo IRFoo)
  {
    _tag = Tag.IRFoo;
    _data.IRFoo = IRFoo;
  }
  public R(IRBar IRBar)
  {
    _tag = Tag.IRBar;
    _data.IRBar = IRBar;
  }
  public R(IRBaz _)
  {
    _tag = Tag.IRBaz;
  }

  public static implicit operator R(IRFoo value) => new(value);
  public static implicit operator R(IRBar value) => new(value);
  public static implicit operator R(IRBaz value) => new(value);

  public readonly bool TryGetValue(out IRFoo value)
  {
    value = _tag == Tag.IRFoo ? _data.IRFoo : default;
    return _tag == Tag.IRFoo;
  }
  public readonly bool TryGetValue(out IRBar value)
  {
    value = _tag == Tag.IRBar ? _data.IRBar : default;
    return _tag == Tag.IRBar;
  }
  public readonly bool TryGetValue(out IRBaz value)
  {
    value = default;
    return _tag == Tag.IRBaz;
  }

  public override readonly bool Equals(object? obj) => obj is R other && Equals(other);
  public readonly bool Equals(R other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.IRFoo => _data.IRFoo.Equals(other._data.IRFoo),
      Tag.IRBar => _data.IRBar.Equals(other._data.IRBar),
      _ => true,

    };

  }
  public static bool operator ==(R left, R right) => left.Equals(right);
  public static bool operator !=(R left, R right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.IRFoo:
      {
        hashCode.Add(_data.IRFoo);
        break;
      }
      case Tag.IRBar:
      {
        hashCode.Add(_data.IRBar);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.IRFoo:
      {
        stringBuilder.Append(" { ");
        _data.IRFoo.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.IRBar:
      {
        stringBuilder.Append(" { ");
        _data.IRBar.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct R_Data
  {
    [FieldOffset(0)]
    public IRFoo IRFoo;
    [FieldOffset(0)]
    public IRBar IRBar;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct IRFoo()
  {
    public required short Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public IRFoo(short value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct IRBar()
  {
    public required byte x;
    public required short y;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public IRBar(byte x, short y) : this()
    {
      this.x = x;
      this.y = y;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  public record struct IRBaz();

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
