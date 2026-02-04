using System.Runtime.InteropServices;
using IntFoo_i32 = Foo_i32__i32;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo_i32__i32
{

  public required int x;
  public required int y;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(IntFoo_i32 a);

}
