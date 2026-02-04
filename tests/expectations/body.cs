using System.Runtime.InteropServices;

public enum MyCLikeEnum
{
  Foo1,
  Bar1,
  Baz1,
};

public enum MyCLikeEnum_Prepended
{
  Foo1_Prepended,
  Bar1_Prepended,
  Baz1_Prepended,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct MyFancyStruct
{

  public required int i;
}

[StructLayout(LayoutKind.Sequential)]
public struct MyFancyEnum
{
  public enum MyFancyEnum_Tag
  {
    Foo,
    Bar,
    Baz,
  };
  private MyFancyEnum_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {


    [StructLayout(LayoutKind.Sequential)]
    public record struct Bar_Body()
    {
      public required int bar;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct Baz_Body()
    {
      public required int baz;
    }
  }

}

[StructLayout(LayoutKind.Explicit)]
public  partial struct MyUnion
{
  [FieldOffset(0)]
  public float f;
  [FieldOffset(0)]
  public uint u;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct MyFancyStruct_Prepended
{

  public required int i;
}

[StructLayout(LayoutKind.Sequential)]
public struct MyFancyEnum_Prepended
{
  public enum MyFancyEnum_Prepended_Tag
  {
    Foo_Prepended,
    Bar_Prepended,
    Baz_Prepended,
  };
  private MyFancyEnum_Prepended_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {


    [StructLayout(LayoutKind.Sequential)]
    public record struct Bar_Prepended_Body()
    {
      public required int bar_prepended;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct Baz_Prepended_Body()
    {
      public required int baz_prepended;
    }
  }

}

[StructLayout(LayoutKind.Explicit)]
public  partial struct MyUnion_Prepended
{
  [FieldOffset(0)]
  public float f;
  [FieldOffset(0)]
  public uint u;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(MyFancyStruct s,
  MyFancyEnum e,
  MyCLikeEnum c,
  MyUnion u,
  MyFancyStruct_Prepended sp,
  MyFancyEnum_Prepended ep,
  MyCLikeEnum_Prepended cp,
  MyUnion_Prepended up);

}
