using System.Runtime.InteropServices;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "A")]
  public safe static partial void A();

  [LibraryImport("library", EntryPoint = "B")]
  public safe static partial void B();

  [LibraryImport("library", EntryPoint = "C")]
  public safe static partial void C();

  [LibraryImport("library", EntryPoint = "D")]
  public safe static partial void D();

}
