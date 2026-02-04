using System.Runtime.InteropServices;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "ptr_as_array")]
  public unsafe static partial void ptr_as_array(uint n, uint* arg, ulong* v);

  [LibraryImport("library", EntryPoint = "ptr_as_array1")]
  public unsafe static partial void ptr_as_array1(uint n, uint* arg, ulong* v);

  [LibraryImport("library", EntryPoint = "ptr_as_array2")]
  public unsafe static partial void ptr_as_array2(uint n, uint* arg, ulong* v);

  [LibraryImport("library", EntryPoint = "ptr_as_array_wrong_syntax")]
  public unsafe static partial void ptr_as_array_wrong_syntax(uint* arg, uint* v, uint* arg2);

  [LibraryImport("library", EntryPoint = "ptr_as_array_unnamed")]
  public unsafe static partial void ptr_as_array_unnamed(uint* arg0, uint* arg1);

}
