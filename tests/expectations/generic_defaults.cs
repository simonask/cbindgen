using System.Runtime.InteropServices;
using Baz_i64 = Foo_i64;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo_i16
{

  public required short field;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo_i32
{

  public required int field;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Bar_i32__u32
{

  public required Foo_i32 f;
  public required uint p;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo_i64
{

  public required long field;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct NeverUsedWithDefault_i32
{

  public required int field;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "foo_root")]
  public unsafe static partial void foo_root(Foo_i16 f, Bar_i32__u32 b, Baz_i64 z);

  [LibraryImport("library", EntryPoint = "with_i32")]
  public unsafe static partial void with_i32(NeverUsedWithDefault_i32 x);

}
