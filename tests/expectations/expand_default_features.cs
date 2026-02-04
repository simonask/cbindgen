using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{


}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "extra_debug_fn")]
  public unsafe static partial void extra_debug_fn();

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo a);

}
