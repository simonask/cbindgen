using System.Runtime.InteropServices;
using Foo = NotReprC_____i32;

public readonly struct NotReprC_____i32
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct MyStruct
{

  public required int* number;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo* a, MyStruct* with_box);

  [LibraryImport("library", EntryPoint = "drop_box")]
  public unsafe static partial void drop_box(int* x);

  [LibraryImport("library", EntryPoint = "drop_box_opt")]
  public unsafe static partial void drop_box_opt(int* x);

}
