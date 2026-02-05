using System.Runtime.InteropServices;

public readonly struct Bar
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{

  public static readonly Foo FOO = new Foo
  {
    a = (int)42,
    b = (uint)47,

  };

  public static readonly Foo FOO2 = new Foo
  {
    a = (int)42,
    b = (uint)47,

  };

  public static readonly Foo FOO3 = new Foo
  {
    a = (int)42,
    b = (uint)47,

  };

  // cbindgen: Skipped 'BAZFoo', because one of its required types is not included

  public required int a;
  public required uint b;
}
public static partial class Api
{
  public static readonly Foo BAR = new Foo
  {
    a = (int)42,
    b = (uint)1337,

  };
  // cbindgen: Skipped 'BAZZ', because one of its required types is not included

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo x, Bar bar);

}
