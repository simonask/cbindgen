using System.Runtime.InteropServices;

public readonly struct I
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
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

  public J(Foo foo)
  {
    _tag = Tag.J_Foo;
    _data.foo = foo;
  }
  public J(Bar bar)
  {
    _tag = Tag.J_Bar;
    _data.bar = bar;
  }
  public J(Baz _)
  {
    _tag = Tag.J_Baz;
  }

  public static implicit operator J(Foo value) => new(value);
  public static implicit operator J(Bar value) => new(value);
  public static implicit operator J(Baz value) => new(value);

  public readonly bool TryGetValue(out Foo value)
  {
    value = _tag == Tag.J_Foo ? _data.foo : default;
    return _tag == Tag.J_Foo;
  }
  public readonly bool TryGetValue(out Bar value)
  {
    value = _tag == Tag.J_Bar ? _data.bar : default;
    return _tag == Tag.J_Bar;
  }
  public readonly bool TryGetValue(out Baz value)
  {
    value = default;
    return _tag == Tag.J_Baz;
  }

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
  private Foo foo;
  [FieldOffset(0)]
  private Bar bar;

  public K(Foo foo)
  {
    this.foo = foo;
  }
  public K(Bar bar)
  {
    this.bar = bar;
  }
  public K(Baz _)
  {
    _tag = Tag.K_Baz;
  }

  public static implicit operator K(Foo value) => new(value);
  public static implicit operator K(Bar value) => new(value);
  public static implicit operator K(Baz value) => new(value);

  public readonly bool TryGetValue(out Foo value)
  {
    value = _tag == Tag.K_Foo ? foo : default;
    return _tag == Tag.K_Foo;
  }
  public readonly bool TryGetValue(out Bar value)
  {
    value = _tag == Tag.K_Bar ? bar : default;
    return _tag == Tag.K_Bar;
  }
  public readonly bool TryGetValue(out Baz value)
  {
    value = default;
    return _tag == Tag.K_Baz;
  }

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
  public record struct Foo()
  {
    private readonly Tag _tag = Tag.K_Foo;
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
    private readonly Tag _tag = Tag.K_Bar;
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
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial void foo(H h, I i, J j, K k);

}
