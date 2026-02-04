using System.Runtime.InteropServices;
using Boo = Foo_____u8;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Foo_____u8
{

  public required byte* a;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo__________u8__________4
{

  public required System.Runtime.CompilerServices.InlineArray4<byte> a;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Boo x);

  [LibraryImport("library", EntryPoint = "my_function")]
  public unsafe static partial void my_function(Foo__________u8__________4 x);

}
