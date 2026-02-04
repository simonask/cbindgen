using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct StylePoint_i32
{

  public required int x;
  public required int y;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct StylePoint_f32
{

  public required float x;
  public required float y;
}

[StructLayout(LayoutKind.Explicit)]
public struct StyleFoo_i32
{
  public enum StyleFoo_i32_Tag : byte
  {
    Foo_i32,
    Bar_i32,
    Baz_i32,
    Bazz_i32,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleFoo_Body_i32()
  {
    private readonly StyleFoo_i32_Tag _tag = StyleFoo_i32_Tag.Foo_i32;
    public required int x;
    public required StylePoint_i32 y;
    public required StylePoint_f32 z;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBar_Body_i32()
  {
    private readonly StyleFoo_i32_Tag _tag = StyleFoo_i32_Tag.Bar_i32;
    public required int bar;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBaz_Body_i32()
  {
    private readonly StyleFoo_i32_Tag _tag = StyleFoo_i32_Tag.Baz_i32;
    public required StylePoint_i32 baz;
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct StyleBar_i32
{
  public enum StyleBar_i32_Tag
  {
    Bar1_i32,
    Bar2_i32,
    Bar3_i32,
    Bar4_i32,
  };
  private StyleBar_i32_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct StyleBar1_Body_i32()
    {
      public required int x;
      public required StylePoint_i32 y;
      public required StylePoint_f32 z;
      public required delegate* unmanaged[Cdecl]<int, int> u;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct StyleBar2_Body_i32()
    {
      public required int bar2;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct StyleBar3_Body_i32()
    {
      public required StylePoint_i32 bar3;
    }

  }

}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct StylePoint_u32
{

  public required uint x;
  public required uint y;
}

[StructLayout(LayoutKind.Sequential)]
public struct StyleBar_u32
{
  public enum StyleBar_u32_Tag
  {
    Bar1_u32,
    Bar2_u32,
    Bar3_u32,
    Bar4_u32,
  };
  private StyleBar_u32_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct StyleBar1_Body_u32()
    {
      public required int x;
      public required StylePoint_u32 y;
      public required StylePoint_f32 z;
      public required delegate* unmanaged[Cdecl]<int, int> u;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct StyleBar2_Body_u32()
    {
      public required uint bar2;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct StyleBar3_Body_u32()
    {
      public required StylePoint_u32 bar3;
    }

  }

}

[StructLayout(LayoutKind.Explicit)]
public struct StyleBaz
{
  public enum StyleBaz_Tag : byte
  {
    Baz1,
    Baz2,
    Baz3,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBaz1_Body()
  {
    private readonly StyleBaz_Tag _tag = StyleBaz_Tag.Baz1;
    public required StyleBar_u32 baz1;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBaz2_Body()
  {
    private readonly StyleBaz_Tag _tag = StyleBaz_Tag.Baz2;
    public required StylePoint_i32 baz2;
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct StyleTaz
{
  public enum StyleTaz_Tag : byte
  {
    Taz1,
    Taz2,
    Taz3,
  };
  private StyleTaz_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public record struct StyleTaz1_Body()
    {
      public required StyleBar_u32 taz1;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct StyleTaz2_Body()
    {
      public required StyleBaz taz2;
    }

  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial void
  foo(StyleFoo_i32* foo,
  StyleBar_i32* bar,
  StyleBaz* baz,
  StyleTaz* taz);

}
