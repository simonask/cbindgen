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
    stringBuilder.Append("H.");
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
    public H_Foo_Body foo;
    [FieldOffset(0)]
    public H_Bar_Body bar;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct H_Foo_Body()
  {
    public required short foo;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct H_Bar_Body()
  {
    public required byte x;
    public required short y;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
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

  public override readonly bool Equals(object? obj) => obj is J other && Equals(other);
  public readonly bool Equals(J other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.J_Foo => _data.foo.Equals(other._data.foo),
      Tag.J_Bar => _data.bar.Equals(other._data.bar),
      _ => true,

    };

  }
  public static bool operator ==(J left, J right) => left.Equals(right);
  public static bool operator !=(J left, J right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.J_Foo:
      {
        hashCode.Add(_data.foo);
        break;
      }
      case Tag.J_Bar:
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
    stringBuilder.Append("J.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.J_Foo:
      {
        stringBuilder.Append(" { ");
        _data.foo.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.J_Bar:
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
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct J_Bar_Body()
  {
    public required byte x;
    public required short y;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
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

  public override readonly bool Equals(object? obj) => obj is K other && Equals(other);
  public readonly bool Equals(K other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.K_Foo => foo.Equals(other.foo),
      Tag.K_Bar => bar.Equals(other.bar),
      _ => true,

    };

  }
  public static bool operator ==(K left, K right) => left.Equals(right);
  public static bool operator !=(K left, K right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    switch (_tag)
    {
      case Tag.K_Foo:
      {
        hashCode.Add(foo);
        break;
      }
      case Tag.K_Bar:
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
    stringBuilder.Append("K.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.K_Foo:
      {
        stringBuilder.Append(" { ");
        foo.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.K_Bar:
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
  public record struct K_Foo_Body()
  {
    private readonly Tag _tag = Tag.K_Foo;
    public required short foo;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct K_Bar_Body()
  {
    private readonly Tag _tag = Tag.K_Bar;
    public required byte x;
    public required short y;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial void foo(H h, I i, J j, K k);

}
