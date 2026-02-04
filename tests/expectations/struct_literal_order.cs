using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct ABC
{

  public static readonly ABC abc = new ABC
  {
    a = (float)1.0,
    b = (uint)2,
    c = (uint)3,

  };

  public static readonly ABC bac = new ABC
  {
    a = (float)1.0,
    b = (uint)2,
    c = (uint)3,

  };

  public static readonly ABC cba = new ABC
  {
    a = (float)1.0,
    b = (uint)2,
    c = (uint)3,

  };

  public required float a;
  public required uint b;
  public required uint c;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct BAC
{

  public static readonly BAC abc = new BAC
  {
    b = (uint)1,
    a = (float)2.0,
    c = (int)3,

  };

  public static readonly BAC bac = new BAC
  {
    b = (uint)1,
    a = (float)2.0,
    c = (int)3,

  };

  public static readonly BAC cba = new BAC
  {
    b = (uint)1,
    a = (float)2.0,
    c = (int)3,

  };

  public required uint b;
  public required float a;
  public required int c;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(ABC a1, BAC a2);

}
