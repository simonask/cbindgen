using System.Runtime.InteropServices;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "first")]
  public unsafe static partial void first();

  [LibraryImport("library", EntryPoint = "second")]
  public unsafe static partial void second();

}
