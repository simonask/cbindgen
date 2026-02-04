using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{

  public required InlineArray_FOO_int x;
}
public static partial class Api
{

  public const int FOO = (int)10;

  public const float ZOM = (float)3.14;

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo x);

}
[System.Runtime.CompilerServices.InlineArray((int)Api.FOO)]
public struct InlineArray_FOO_int
{
  private int _data;
}
