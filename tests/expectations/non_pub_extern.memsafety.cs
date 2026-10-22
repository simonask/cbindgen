using System.Runtime.InteropServices;
public static partial class Api
{

  // cbindgen: Skipped 'FIRST', because it is a static extern.

  // cbindgen: Skipped 'RENAMED', because it is a static extern.

  [LibraryImport("library", EntryPoint = "first")]
  public safe static partial void first();

  [LibraryImport("library", EntryPoint = "renamed")]
  public safe static partial void renamed();

}
