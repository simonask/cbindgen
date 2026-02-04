using System.Runtime.InteropServices;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "A")]
  public unsafe static partial void A();

  [LibraryImport("library", EntryPoint = "B")]
  public unsafe static partial void B();

  [LibraryImport("library", EntryPoint = "C")]
  public unsafe static partial void C();

  [LibraryImport("library", EntryPoint = "D")]
  public unsafe static partial void D();

}
