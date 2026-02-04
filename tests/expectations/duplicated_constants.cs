using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{

  public const uint Foo_FIELD_RELATED_CONSTANT = (uint)0;
  public required uint field;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Bar
{

  public const uint Bar_FIELD_RELATED_CONSTANT = (uint)0;
  public required uint field;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo a, Bar b);

}
