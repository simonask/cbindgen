using System.Runtime.InteropServices;

public enum FillRule : byte
{
  A,
  B,
};

/// <summary>
///      This will have a destructor manually implemented via variant_body, and
///      similarly a Drop impl in Rust.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct OwnedSlice_u32
{

  public required nuint len;
  public required uint* ptr;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Polygon_u32
{

  public required FillRule fill;
  public required OwnedSlice_u32 coordinates;
}

/// <summary>
///      This will have a destructor manually implemented via variant_body, and
///      similarly a Drop impl in Rust.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct OwnedSlice_i32
{

  public required nuint len;
  public required int* ptr;
}

[StructLayout(LayoutKind.Sequential)]
public struct Foo_u32
{
  public enum Tag : byte
  {
    Bar_u32,
    Polygon1_u32,
    Slice1_u32,
    Slice2_u32,
    Slice3_u32,
    Slice4_u32,
  };
  private Tag _tag;
  private Foo_u32_Data _data;

  public static Foo_u32 Bar_u32() => new() { _tag = Tag.Bar_u32 };
  public static Foo_u32 Polygon1_u32(Polygon_u32 polygon1)
     => new(new Polygon1_Body_u32
      {
        polygon1 = polygon1,

      });
  public static Foo_u32 Slice1_u32(OwnedSlice_u32 slice1)
     => new(new Slice1_Body_u32
      {
        slice1 = slice1,

      });
  public static Foo_u32 Slice2_u32(OwnedSlice_i32 slice2)
     => new(new Slice2_Body_u32
      {
        slice2 = slice2,

      });
  public static Foo_u32 Slice3_u32(FillRule fill, OwnedSlice_u32 coords)
     => new(new Slice3_Body_u32
      {
        fill = fill,
        coords = coords,

      });
  public static Foo_u32 Slice4_u32(FillRule fill, OwnedSlice_i32 coords)
     => new(new Slice4_Body_u32
      {
        fill = fill,
        coords = coords,

      });


  public Foo_u32(Polygon1_Body_u32 polygon1)
  {
    _tag = Tag.Polygon1_u32;
    _data.polygon1 = polygon1;
  }
  public Foo_u32(Slice1_Body_u32 slice1)
  {
    _tag = Tag.Slice1_u32;
    _data.slice1 = slice1;
  }
  public Foo_u32(Slice2_Body_u32 slice2)
  {
    _tag = Tag.Slice2_u32;
    _data.slice2 = slice2;
  }
  public Foo_u32(Slice3_Body_u32 slice3)
  {
    _tag = Tag.Slice3_u32;
    _data.slice3 = slice3;
  }
  public Foo_u32(Slice4_Body_u32 slice4)
  {
    _tag = Tag.Slice4_u32;
    _data.slice4 = slice4;
  }

  public static implicit operator Foo_u32(Polygon1_Body_u32 value) => new(value);
  public static implicit operator Foo_u32(Slice1_Body_u32 value) => new(value);
  public static implicit operator Foo_u32(Slice2_Body_u32 value) => new(value);
  public static implicit operator Foo_u32(Slice3_Body_u32 value) => new(value);
  public static implicit operator Foo_u32(Slice4_Body_u32 value) => new(value);

  public readonly bool IsBar_u32 => _tag == Tag.Bar_u32;
  public readonly bool IsPolygon1_u32 => _tag == Tag.Polygon1_u32;
  public readonly bool IsSlice1_u32 => _tag == Tag.Slice1_u32;
  public readonly bool IsSlice2_u32 => _tag == Tag.Slice2_u32;
  public readonly bool IsSlice3_u32 => _tag == Tag.Slice3_u32;
  public readonly bool IsSlice4_u32 => _tag == Tag.Slice4_u32;

  public readonly Polygon1_Body_u32? AsPolygon1_u32 => _tag == Tag.Polygon1_u32 ? _data.polygon1 : null;
  public readonly Slice1_Body_u32? AsSlice1_u32 => _tag == Tag.Slice1_u32 ? _data.slice1 : null;
  public readonly Slice2_Body_u32? AsSlice2_u32 => _tag == Tag.Slice2_u32 ? _data.slice2 : null;
  public readonly Slice3_Body_u32? AsSlice3_u32 => _tag == Tag.Slice3_u32 ? _data.slice3 : null;
  public readonly Slice4_Body_u32? AsSlice4_u32 => _tag == Tag.Slice4_u32 ? _data.slice4 : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct Foo_u32_Data
  {
    [FieldOffset(0)]
    public Polygon1_Body_u32 polygon1;
    [FieldOffset(0)]
    public Slice1_Body_u32 slice1;
    [FieldOffset(0)]
    public Slice2_Body_u32 slice2;
    [FieldOffset(0)]
    public Slice3_Body_u32 slice3;
    [FieldOffset(0)]
    public Slice4_Body_u32 slice4;
  }

  [StructLayout(LayoutKind.Sequential)]
  public record struct Polygon1_Body_u32()
  {
    public required Polygon_u32 polygon1;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice1_Body_u32()
  {
    public required OwnedSlice_u32 slice1;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice2_Body_u32()
  {
    public required OwnedSlice_i32 slice2;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice3_Body_u32()
  {
    public required FillRule fill;
    public required OwnedSlice_u32 coords;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice4_Body_u32()
  {
    public required FillRule fill;
    public required OwnedSlice_i32 coords;
  }

}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Polygon_i32
{

  public required FillRule fill;
  public required OwnedSlice_i32 coordinates;
}

[StructLayout(LayoutKind.Explicit)]
public struct Baz_i32
{
  public enum Tag : byte
  {
    Bar2_i32,
    Polygon21_i32,
    Slice21_i32,
    Slice22_i32,
    Slice23_i32,
    Slice24_i32,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Polygon21_Body_i32 polygon21;
  [FieldOffset(0)]
  private Slice21_Body_i32 slice21;
  [FieldOffset(0)]
  private Slice22_Body_i32 slice22;
  [FieldOffset(0)]
  private Slice23_Body_i32 slice23;
  [FieldOffset(0)]
  private Slice24_Body_i32 slice24;

  public static Baz_i32 Bar2_i32() => new() { _tag = Tag.Bar2_i32 };
  public static Baz_i32 Polygon21_i32(Polygon_i32 polygon21)
     => new(new Polygon21_Body_i32
      {
        polygon21 = polygon21,

      });
  public static Baz_i32 Slice21_i32(OwnedSlice_i32 slice21)
     => new(new Slice21_Body_i32
      {
        slice21 = slice21,

      });
  public static Baz_i32 Slice22_i32(OwnedSlice_i32 slice22)
     => new(new Slice22_Body_i32
      {
        slice22 = slice22,

      });
  public static Baz_i32 Slice23_i32(FillRule fill, OwnedSlice_i32 coords)
     => new(new Slice23_Body_i32
      {
        fill = fill,
        coords = coords,

      });
  public static Baz_i32 Slice24_i32(FillRule fill, OwnedSlice_i32 coords)
     => new(new Slice24_Body_i32
      {
        fill = fill,
        coords = coords,

      });


  public Baz_i32(Polygon21_Body_i32 polygon21)
  {
    this.polygon21 = polygon21;
  }
  public Baz_i32(Slice21_Body_i32 slice21)
  {
    this.slice21 = slice21;
  }
  public Baz_i32(Slice22_Body_i32 slice22)
  {
    this.slice22 = slice22;
  }
  public Baz_i32(Slice23_Body_i32 slice23)
  {
    this.slice23 = slice23;
  }
  public Baz_i32(Slice24_Body_i32 slice24)
  {
    this.slice24 = slice24;
  }

  public static implicit operator Baz_i32(Polygon21_Body_i32 value) => new(value);
  public static implicit operator Baz_i32(Slice21_Body_i32 value) => new(value);
  public static implicit operator Baz_i32(Slice22_Body_i32 value) => new(value);
  public static implicit operator Baz_i32(Slice23_Body_i32 value) => new(value);
  public static implicit operator Baz_i32(Slice24_Body_i32 value) => new(value);

  public readonly bool IsBar2_i32 => _tag == Tag.Bar2_i32;
  public readonly bool IsPolygon21_i32 => _tag == Tag.Polygon21_i32;
  public readonly bool IsSlice21_i32 => _tag == Tag.Slice21_i32;
  public readonly bool IsSlice22_i32 => _tag == Tag.Slice22_i32;
  public readonly bool IsSlice23_i32 => _tag == Tag.Slice23_i32;
  public readonly bool IsSlice24_i32 => _tag == Tag.Slice24_i32;

  public readonly Polygon21_Body_i32? AsPolygon21_i32 => _tag == Tag.Polygon21_i32 ? polygon21 : null;
  public readonly Slice21_Body_i32? AsSlice21_i32 => _tag == Tag.Slice21_i32 ? slice21 : null;
  public readonly Slice22_Body_i32? AsSlice22_i32 => _tag == Tag.Slice22_i32 ? slice22 : null;
  public readonly Slice23_Body_i32? AsSlice23_i32 => _tag == Tag.Slice23_i32 ? slice23 : null;
  public readonly Slice24_Body_i32? AsSlice24_i32 => _tag == Tag.Slice24_i32 ? slice24 : null;



  [StructLayout(LayoutKind.Sequential)]
  public record struct Polygon21_Body_i32()
  {
    private readonly Tag _tag = Tag.Polygon21_i32;
    public required Polygon_i32 polygon21;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice21_Body_i32()
  {
    private readonly Tag _tag = Tag.Slice21_i32;
    public required OwnedSlice_i32 slice21;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice22_Body_i32()
  {
    private readonly Tag _tag = Tag.Slice22_i32;
    public required OwnedSlice_i32 slice22;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice23_Body_i32()
  {
    private readonly Tag _tag = Tag.Slice23_i32;
    public required FillRule fill;
    public required OwnedSlice_i32 coords;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice24_Body_i32()
  {
    private readonly Tag _tag = Tag.Slice24_i32;
    public required FillRule fill;
    public required OwnedSlice_i32 coords;
  }
}

[StructLayout(LayoutKind.Explicit)]
public struct Taz
{
  public enum Tag : byte
  {
    Bar3,
    Taz1,
    Taz3,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Taz1_Body taz1;
  [FieldOffset(0)]
  private Taz3_Body taz3;

  public static Taz Bar3() => new() { _tag = Tag.Bar3 };
  public static Taz Taz1(int taz1)
     => new(new Taz1_Body
      {
        taz1 = taz1,

      });
  public static Taz Taz3(OwnedSlice_i32 taz3)
     => new(new Taz3_Body
      {
        taz3 = taz3,

      });


  public Taz(Taz1_Body taz1)
  {
    this.taz1 = taz1;
  }
  public Taz(Taz3_Body taz3)
  {
    this.taz3 = taz3;
  }

  public static implicit operator Taz(Taz1_Body value) => new(value);
  public static implicit operator Taz(Taz3_Body value) => new(value);

  public readonly bool IsBar3 => _tag == Tag.Bar3;
  public readonly bool IsTaz1 => _tag == Tag.Taz1;
  public readonly bool IsTaz3 => _tag == Tag.Taz3;

  public readonly Taz1_Body? AsTaz1 => _tag == Tag.Taz1 ? taz1 : null;
  public readonly Taz3_Body? AsTaz3 => _tag == Tag.Taz3 ? taz3 : null;



  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz1_Body()
  {
    private readonly Tag _tag = Tag.Taz1;
    public required int taz1;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz3_Body()
  {
    private readonly Tag _tag = Tag.Taz3;
    public required OwnedSlice_i32 taz3;
  }
}

[StructLayout(LayoutKind.Explicit)]
public struct Tazz
{
  public enum Tag : byte
  {
    Bar4,
    Taz2,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Taz2_Body taz2;

  public static Tazz Bar4() => new() { _tag = Tag.Bar4 };
  public static Tazz Taz2(int taz2)
     => new(new Taz2_Body
      {
        taz2 = taz2,

      });


  public Tazz(Taz2_Body taz2)
  {
    this.taz2 = taz2;
  }

  public static implicit operator Tazz(Taz2_Body value) => new(value);

  public readonly bool IsBar4 => _tag == Tag.Bar4;
  public readonly bool IsTaz2 => _tag == Tag.Taz2;

  public readonly Taz2_Body? AsTaz2 => _tag == Tag.Taz2 ? taz2 : null;



  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz2_Body()
  {
    private readonly Tag _tag = Tag.Taz2;
    public required int taz2;
  }
}

[StructLayout(LayoutKind.Explicit)]
public struct Tazzz
{
  public enum Tag : byte
  {
    Bar5,
    Taz5,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Taz5_Body taz5;

  public static Tazzz Bar5() => new() { _tag = Tag.Bar5 };
  public static Tazzz Taz5(int taz5)
     => new(new Taz5_Body
      {
        taz5 = taz5,

      });


  public Tazzz(Taz5_Body taz5)
  {
    this.taz5 = taz5;
  }

  public static implicit operator Tazzz(Taz5_Body value) => new(value);

  public readonly bool IsBar5 => _tag == Tag.Bar5;
  public readonly bool IsTaz5 => _tag == Tag.Taz5;

  public readonly Taz5_Body? AsTaz5 => _tag == Tag.Taz5 ? taz5 : null;



  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz5_Body()
  {
    private readonly Tag _tag = Tag.Taz5;
    public required int taz5;
  }
}

[StructLayout(LayoutKind.Explicit)]
public struct Tazzzz
{
  public enum Tag : byte
  {
    Taz6,
    Taz7,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Taz6_Body taz6;
  [FieldOffset(0)]
  private Taz7_Body taz7;

  public static Tazzzz Taz6(int taz6)
     => new(new Taz6_Body
      {
        taz6 = taz6,

      });
  public static Tazzzz Taz7(uint taz7)
     => new(new Taz7_Body
      {
        taz7 = taz7,

      });

  public Tazzzz(Taz6_Body taz6)
  {
    this.taz6 = taz6;
  }
  public Tazzzz(Taz7_Body taz7)
  {
    this.taz7 = taz7;
  }

  public static implicit operator Tazzzz(Taz6_Body value) => new(value);
  public static implicit operator Tazzzz(Taz7_Body value) => new(value);

  public readonly bool IsTaz6 => _tag == Tag.Taz6;
  public readonly bool IsTaz7 => _tag == Tag.Taz7;

  public readonly Taz6_Body? AsTaz6 => _tag == Tag.Taz6 ? taz6 : null;
  public readonly Taz7_Body? AsTaz7 => _tag == Tag.Taz7 ? taz7 : null;


  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz6_Body()
  {
    private readonly Tag _tag = Tag.Taz6;
    public required int taz6;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz7_Body()
  {
    private readonly Tag _tag = Tag.Taz7;
    public required uint taz7;
  }
}

[StructLayout(LayoutKind.Explicit)]
public struct Qux
{
  public enum Tag : byte
  {
    Qux1,
    Qux2,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Qux1_Body qux1;
  [FieldOffset(0)]
  private Qux2_Body qux2;

  public static Qux Qux1(int qux1)
     => new(new Qux1_Body
      {
        qux1 = qux1,

      });
  public static Qux Qux2(uint qux2)
     => new(new Qux2_Body
      {
        qux2 = qux2,

      });

  public Qux(Qux1_Body qux1)
  {
    this.qux1 = qux1;
  }
  public Qux(Qux2_Body qux2)
  {
    this.qux2 = qux2;
  }

  public static implicit operator Qux(Qux1_Body value) => new(value);
  public static implicit operator Qux(Qux2_Body value) => new(value);

  public readonly bool IsQux1 => _tag == Tag.Qux1;
  public readonly bool IsQux2 => _tag == Tag.Qux2;

  public readonly Qux1_Body? AsQux1 => _tag == Tag.Qux1 ? qux1 : null;
  public readonly Qux2_Body? AsQux2 => _tag == Tag.Qux2 ? qux2 : null;


  [StructLayout(LayoutKind.Sequential)]
  public record struct Qux1_Body()
  {
    private readonly Tag _tag = Tag.Qux1;
    public required int qux1;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Qux2_Body()
  {
    private readonly Tag _tag = Tag.Qux2;
    public required uint qux2;
  }
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(Foo_u32* a,
    Baz_i32* b,
    Taz* c,
    Tazz d,
    Tazzz* e,
    Tazzzz* f,
    Qux* g);

}
