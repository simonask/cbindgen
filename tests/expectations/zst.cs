using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct TraitObject
{

  public required void* data;
  public required void* vtable;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void* root(void* ptr, TraitObject t);

}
