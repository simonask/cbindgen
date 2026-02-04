using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct A
{

  public required int* data;
}

[StructLayout(LayoutKind.Sequential)]
public struct E
{
  public enum E_Tag
  {
    V,
    U,
  };
  private E_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {


    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct U_Body()
    {
      public required byte* u;
    }
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A _a, E _e);

}
