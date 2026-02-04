using System.Runtime.InteropServices;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "c")]
  public unsafe static partial void c();

  [LibraryImport("library", EntryPoint = "c_unwind")]
  public unsafe static partial void c_unwind();

}
