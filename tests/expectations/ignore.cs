using System.Runtime.InteropServices;
public static partial class Api
{

  public const uint NO_IGNORE_CONST = (uint)0;

  public const uint NoIgnoreStructWithImpl_NO_IGNORE_INNER_CONST = (uint)0;

  [LibraryImport("library", EntryPoint = "no_ignore_root")]
  public unsafe static partial void no_ignore_root();

  [LibraryImport("library", EntryPoint = "no_ignore_associated_method")]
  public unsafe static partial void no_ignore_associated_method();

}
