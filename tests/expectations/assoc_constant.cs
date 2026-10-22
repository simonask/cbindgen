using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{

  public const int Foo_GA = (int)10;
  public static ReadOnlySpan<byte> BU => "hello world"u8;

  public const float Foo_ZO = (float)3.14;

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo x);

}
