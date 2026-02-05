using System.Runtime.InteropServices;
public static partial class Api
{

  /// <summary>
  ///      The root of all evil.
  /// </summary>
  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root();

  /// <summary>
  ///      A little above the root, and a lot more visible, with a run-on sentence
  /// </summary>
  [LibraryImport("library", EntryPoint = "trunk")]
  public unsafe static partial void trunk();

}
