using System.Runtime.InteropServices;

public readonly struct StyleA
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct B
{

  public required int x;
  public required float y;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(StyleA* a, B b);

}
