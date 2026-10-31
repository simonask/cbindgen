using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial  struct Foo_Bar
{

  public required int* something;
}

[StructLayout(LayoutKind.Explicit)]
public  partial struct Bar
{
  [FieldOffset(0)]
  public unsafe int something;
  [FieldOffset(0)]
  public unsafe Foo_Bar subexpressions;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public safe static partial void root(Bar b);

}
