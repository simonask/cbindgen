using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct A
{

  public required int @namespace;
  public required float @float;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct B
{

  public required int @namespace;
  public required float @float;
}

[StructLayout(LayoutKind.Sequential)]
public struct C
{
  public enum Tag : byte
  {
    D,
  };
  private Tag _tag;
  private C_Data _data;

  public static C D(int @namespace, float @float)
     => new(new D_Body
      {
        @namespace = @namespace,
        @float = @float,

      });

  public C(D_Body d)
  {
    _tag = Tag.D;
    _data.d = d;
  }

  public static implicit operator C(D_Body value) => new(value);

  public readonly bool IsD => _tag == Tag.D;

  public readonly D_Body? AsD => _tag == Tag.D ? _data.d : null;

  public override readonly bool Equals(object? obj) => obj is C other && Equals(other);
  public readonly bool Equals(C other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.D => _data.d.Equals(other._data.d),
      _ => true,

    };

  }
  public static bool operator ==(C left, C right) => left.Equals(right);
  public static bool operator !=(C left, C right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.D:
      {
        hashCode.Add(_data.d);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append("C.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.D:
      {
        stringBuilder.Append(" { ");
        _data.d.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct C_Data
  {
    [FieldOffset(0)]
    public D_Body d;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct D_Body()
  {
    public required int @namespace;
    public required float @float;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct E
{
  public enum Tag : byte
  {
    Double,
    Float,
  };
  private Tag _tag;
  private E_Data _data;

  public static E Double(double @double)
     => new(new Double_Body
      {
        @double = @double,

      });
  public static E Float(float @float)
     => new(new Float_Body
      {
        @float = @float,

      });

  public E(Double_Body @double)
  {
    _tag = Tag.Double;
    _data.@double = @double;
  }
  public E(Float_Body @float)
  {
    _tag = Tag.Float;
    _data.@float = @float;
  }

  public static implicit operator E(Double_Body value) => new(value);
  public static implicit operator E(Float_Body value) => new(value);

  public readonly bool IsDouble => _tag == Tag.Double;
  public readonly bool IsFloat => _tag == Tag.Float;

  public readonly Double_Body? AsDouble => _tag == Tag.Double ? _data.@double : null;
  public readonly Float_Body? AsFloat => _tag == Tag.Float ? _data.@float : null;

  public override readonly bool Equals(object? obj) => obj is E other && Equals(other);
  public readonly bool Equals(E other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.Double => _data.@double.Equals(other._data.@double),
      Tag.Float => _data.@float.Equals(other._data.@float),
      _ => true,

    };

  }
  public static bool operator ==(E left, E right) => left.Equals(right);
  public static bool operator !=(E left, E right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.Double:
      {
        hashCode.Add(_data.@double);
        break;
      }
      case Tag.Float:
      {
        hashCode.Add(_data.@float);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append("E.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.Double:
      {
        stringBuilder.Append(" { ");
        _data.@double.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.Float:
      {
        stringBuilder.Append(" { ");
        _data.@float.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct E_Data
  {
    [FieldOffset(0)]
    public Double_Body @double;
    [FieldOffset(0)]
    public Float_Body @float;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Double_Body()
  {
    public required double @double;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Float_Body()
  {
    public required float @float;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct F
{
  public enum Tag : byte
  {
    @double,
    @float,
  };
  private Tag _tag;
  private F_Data _data;

  public static F @double(double @double)
     => new(new double_Body
      {
        @double = @double,

      });
  public static F @float(float @float)
     => new(new float_Body
      {
        @float = @float,

      });

  public F(double_Body @double)
  {
    _tag = Tag.@double;
    _data.@double = @double;
  }
  public F(float_Body @float)
  {
    _tag = Tag.@float;
    _data.@float = @float;
  }

  public static implicit operator F(double_Body value) => new(value);
  public static implicit operator F(float_Body value) => new(value);

  public readonly bool Isdouble => _tag == Tag.@double;
  public readonly bool Isfloat => _tag == Tag.@float;

  public readonly double_Body? Asdouble => _tag == Tag.@double ? _data.@double : null;
  public readonly float_Body? Asfloat => _tag == Tag.@float ? _data.@float : null;

  public override readonly bool Equals(object? obj) => obj is F other && Equals(other);
  public readonly bool Equals(F other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.@double => _data.@double.Equals(other._data.@double),
      Tag.@float => _data.@float.Equals(other._data.@float),
      _ => true,

    };

  }
  public static bool operator ==(F left, F right) => left.Equals(right);
  public static bool operator !=(F left, F right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.@double:
      {
        hashCode.Add(_data.@double);
        break;
      }
      case Tag.@float:
      {
        hashCode.Add(_data.@float);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append("F.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.@double:
      {
        stringBuilder.Append(" { ");
        _data.@double.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.@float:
      {
        stringBuilder.Append(" { ");
        _data.@float.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct F_Data
  {
    [FieldOffset(0)]
    public double_Body @double;
    [FieldOffset(0)]
    public float_Body @float;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct double_Body()
  {
    public required double @double;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct float_Body()
  {
    public required float @float;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A a, B b, C c, E e, F f, int @namespace, float @float);

}
