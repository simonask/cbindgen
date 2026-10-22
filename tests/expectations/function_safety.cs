using System.Runtime.InteropServices;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "safe_export")]
  public unsafe static partial void safe_export(int* a);

  [LibraryImport("library", EntryPoint = "unsafe_export")]
  public unsafe static partial void unsafe_export(int* a);

  [LibraryImport("library", EntryPoint = "legacy_import")]
  public unsafe static partial void legacy_import(int a);

  [LibraryImport("library", EntryPoint = "safe_import")]
  public unsafe static partial void safe_import(int a);

  [LibraryImport("library", EntryPoint = "unsafe_import")]
  public unsafe static partial void unsafe_import(int* a);

  [LibraryImport("library", EntryPoint = "default_import")]
  public unsafe static partial void default_import(int a);

}
