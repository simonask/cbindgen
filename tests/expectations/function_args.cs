using System.Runtime.InteropServices;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "unnamed")]
  public unsafe static partial void unnamed(ulong* arg0);

  [LibraryImport("library", EntryPoint = "pointer_test")]
  public unsafe static partial void pointer_test(ulong* a);

  [LibraryImport("library", EntryPoint = "print_from_rust")]
  public unsafe static partial void print_from_rust();

}
