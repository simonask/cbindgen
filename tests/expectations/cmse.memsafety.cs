using System.Runtime.InteropServices;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "foo")]
  public safe static partial void foo();

  [LibraryImport("library", EntryPoint = "bar")]
  public safe static partial void bar();

}
