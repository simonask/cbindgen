using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Normal
{

  public required int x;
  public required float y;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial int foo();

  [LibraryImport("library", EntryPoint = "bar")]
  public unsafe static partial void bar(Normal a);

  [LibraryImport("library", EntryPoint = "baz")]
  public unsafe static partial int baz();

}
