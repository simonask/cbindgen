using System.Runtime.InteropServices;
using Foo = NotReprC______i32;

public readonly struct NotReprC______i32
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
  public unsafe static partial void root(Foo* a, MyStruct* with_maybe_uninit);

}
