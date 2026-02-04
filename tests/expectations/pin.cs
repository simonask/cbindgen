using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct PinTest
{

  public required int* pinned_box;
  public required int* pinned_ref;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(int* s, PinTest p);

}
