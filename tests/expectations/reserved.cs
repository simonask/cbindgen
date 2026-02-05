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
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Float_Body()
  {
    public required float @float;
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
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct float_Body()
  {
    public required float @float;
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A a, B b, C c, E e, F f, int @namespace, float @float);

}
