using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct TakeUntil_0
{

  public required byte* start;
  public required nuint len;
  public required nuint point;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "until_nul")]
  public unsafe static partial TakeUntil_0 until_nul(byte* start, nuint len);

}
