using System.Runtime.InteropServices;

public readonly struct Bar
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{


}
public static partial class Api
{

  // cbindgen: Skipped 'NUMBER', because it is a static extern.

  // cbindgen: Skipped 'FOO', because it is a static extern.

  // cbindgen: Skipped 'BAR', because it is a static extern.

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root();

}
