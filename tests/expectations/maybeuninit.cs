using System.Runtime.InteropServices;
using Foo = NotReprC______i32;

public readonly struct NotReprC______i32
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
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
