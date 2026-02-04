using System.Runtime.InteropServices;

#if (NOT_DEFINED || DEFINED)
[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{

  public required int x;
}
#endif

#if NOT_DEFINED
[StructLayout(LayoutKind.Sequential)]
public  partial record struct Bar
{

  public required Foo y;
}
#endif

#if DEFINED
[StructLayout(LayoutKind.Sequential)]
public  partial record struct Bar
{

  public required Foo z;
}
#endif

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Root
{

  public required Bar w;
}
public static partial class Api
{

#if NOT_DEFINED
  public const int DEFAULT_X = (int)8;
#endif

#if DEFINED
  public const int DEFAULT_X = (int)42;
#endif

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Root a);

}
