using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct PREFIXBar
{

  public required int a;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct PREFIXFoo
{

  public required int a;
  public required uint b;
  public required PREFIXBar bar;
}
public static partial class Api
{
  public static readonly PREFIXFoo PREFIXVAL = new PREFIXFoo
  {
    a = (int)42,
    b = (uint)1337,
    bar = (PREFIXBar)new PREFIXBar
    {
      a = (int)323,

    },

  };

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(PREFIXFoo x);

}
