using System.Runtime.InteropServices;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "test_camel_case")]
  public unsafe static partial void test_camel_case(int fooBar);

  [LibraryImport("library", EntryPoint = "test_pascal_case")]
  public unsafe static partial void test_pascal_case(int FooBar);

  [LibraryImport("library", EntryPoint = "test_snake_case")]
  public unsafe static partial void test_snake_case(int foo_bar);

  [LibraryImport("library", EntryPoint = "test_screaming_snake_case")]
  public unsafe static partial void test_screaming_snake_case(int FOO_BAR);

  [LibraryImport("library", EntryPoint = "test_gecko_case")]
  public unsafe static partial void test_gecko_case(int aFooBar);

  [LibraryImport("library", EntryPoint = "test_prefix")]
  public unsafe static partial void test_prefix(int prefix_foo_bar);

}
