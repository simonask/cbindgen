using System.Runtime.InteropServices;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial void foo();

  [LibraryImport("library", EntryPoint = "bar")]
  public unsafe static partial void bar();

}
