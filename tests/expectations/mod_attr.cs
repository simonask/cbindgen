using System.Runtime.InteropServices;

#if FOO
[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{


}
#endif

#if BAR
[StructLayout(LayoutKind.Sequential)]
public  partial record struct Bar
{


}
#endif
public static partial class Api
{

#if FOO
  public const int FOO = (int)1;
#endif

#if BAR
  public const int BAR = (int)2;
#endif

#if FOO
  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial void foo(Foo* foo);
#endif

#if BAR
  [LibraryImport("library", EntryPoint = "bar")]
  public unsafe static partial void bar(Bar* bar);
#endif

}
