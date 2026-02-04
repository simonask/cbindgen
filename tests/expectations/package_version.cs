// Package version: 0.1.0
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{

  public required ulong bar;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "doit")]
  public unsafe static partial void doit(Foo* arg0);

}
