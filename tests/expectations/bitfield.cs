using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct HasBitfields
{

  public required ulong foo;
  public required ulong bar;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(HasBitfields* arg0);

}
