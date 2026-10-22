using System.Runtime.InteropServices;

public readonly struct StyleA
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
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
