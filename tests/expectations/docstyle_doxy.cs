using System.Runtime.InteropServices;
public static partial class Api
{

  /// <summary>
  /// ///  The root of all evil.
  /// </summary>
  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root();

}
