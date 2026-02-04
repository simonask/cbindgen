using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct dep_struct
{

  public required uint x;
  public required double y;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "get_x")]
  public unsafe static partial uint get_x(dep_struct* dep_struct);

}
