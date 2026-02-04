using System.Runtime.InteropServices;

public enum FillRule : byte
{
  A,
  B,
};

/// <summary>
/// ///  This will have a destructor manually implemented via variant_body, and
/// ///  similarly a Drop impl in Rust.
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
/// ///  This will have a destructor manually implemented via variant_body, and
/// ///  similarly a Drop impl in Rust.
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
  public enum Foo_u32_Tag : byte
  {
    Bar_u32,
    Polygon1_u32,
    Slice1_u32,
    Slice2_u32,
    Slice3_u32,
    Slice4_u32,
  };
  private Foo_u32_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {


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
  public enum Baz_i32_Tag : byte
  {
    Bar2_i32,
    Polygon21_i32,
    Slice21_i32,
    Slice22_i32,
    Slice23_i32,
    Slice24_i32,
  };


  [StructLayout(LayoutKind.Sequential)]
  public record struct Polygon21_Body_i32()
  {
    private readonly Baz_i32_Tag _tag = Baz_i32_Tag.Polygon21_i32;
    public required Polygon_i32 polygon21;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice21_Body_i32()
  {
    private readonly Baz_i32_Tag _tag = Baz_i32_Tag.Slice21_i32;
    public required OwnedSlice_i32 slice21;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice22_Body_i32()
  {
    private readonly Baz_i32_Tag _tag = Baz_i32_Tag.Slice22_i32;
    public required OwnedSlice_i32 slice22;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice23_Body_i32()
  {
    private readonly Baz_i32_Tag _tag = Baz_i32_Tag.Slice23_i32;
    public required FillRule fill;
    public required OwnedSlice_i32 coords;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Slice24_Body_i32()
  {
    private readonly Baz_i32_Tag _tag = Baz_i32_Tag.Slice24_i32;
    public required FillRule fill;
    public required OwnedSlice_i32 coords;
  }
}

[StructLayout(LayoutKind.Explicit)]
public struct Taz
{
  public enum Taz_Tag : byte
  {
    Bar3,
    Taz1,
    Taz3,
  };


  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz1_Body()
  {
    private readonly Taz_Tag _tag = Taz_Tag.Taz1;
    public required int taz1;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz3_Body()
  {
    private readonly Taz_Tag _tag = Taz_Tag.Taz3;
    public required OwnedSlice_i32 taz3;
  }
}

[StructLayout(LayoutKind.Explicit)]
public struct Tazz
{
  public enum Tazz_Tag : byte
  {
    Bar4,
    Taz2,
  };


  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz2_Body()
  {
    private readonly Tazz_Tag _tag = Tazz_Tag.Taz2;
    public required int taz2;
  }
}

[StructLayout(LayoutKind.Explicit)]
public struct Tazzz
{
  public enum Tazzz_Tag : byte
  {
    Bar5,
    Taz5,
  };


  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz5_Body()
  {
    private readonly Tazzz_Tag _tag = Tazzz_Tag.Taz5;
    public required int taz5;
  }
}

[StructLayout(LayoutKind.Explicit)]
public struct Tazzzz
{
  public enum Tazzzz_Tag : byte
  {
    Taz6,
    Taz7,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz6_Body()
  {
    private readonly Tazzzz_Tag _tag = Tazzzz_Tag.Taz6;
    public required int taz6;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Taz7_Body()
  {
    private readonly Tazzzz_Tag _tag = Tazzzz_Tag.Taz7;
    public required uint taz7;
  }
}

[StructLayout(LayoutKind.Explicit)]
public struct Qux
{
  public enum Qux_Tag : byte
  {
    Qux1,
    Qux2,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct Qux1_Body()
  {
    private readonly Qux_Tag _tag = Qux_Tag.Qux1;
    public required int qux1;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Qux2_Body()
  {
    private readonly Qux_Tag _tag = Qux_Tag.Qux2;
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
