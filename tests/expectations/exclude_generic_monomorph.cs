using Option_Foo = ulong;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Bar
{

  public required Option_Foo foo;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Bar f);

}
