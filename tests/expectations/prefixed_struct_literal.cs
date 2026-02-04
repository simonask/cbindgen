using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct PREFIXFoo
{

  public static readonly PREFIXFoo FOO = new PREFIXFoo
  {
    a = (int)42,
    b = (uint)47,

  };

  public required int a;
  public required uint b;
}
public static partial class Api
{
  public static readonly PREFIXFoo PREFIXBAR = new PREFIXFoo
  {
    a = (int)42,
    b = (uint)1337,

  };

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(PREFIXFoo x);

}
