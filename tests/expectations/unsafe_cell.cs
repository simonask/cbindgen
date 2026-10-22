using System.Runtime.InteropServices;
using Foo = NotReprC_i32;

public readonly struct NotReprC_i32
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct MyStruct
{

  public required int number;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo* a, MyStruct* with_cell);

}
