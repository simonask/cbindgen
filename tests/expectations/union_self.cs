using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Foo_Bar
{

  public required int* something;
}

[StructLayout(LayoutKind.Explicit)]
public  partial struct Bar
{
  [FieldOffset(0)]
  public int something;
  [FieldOffset(0)]
  public Foo_Bar subexpressions;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Bar b);

}
