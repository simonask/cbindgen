using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Foo_Bar
{

  public required int* something;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Bar
{

  public required int something;
  public required Foo_Bar subexpressions;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Bar b);

}
