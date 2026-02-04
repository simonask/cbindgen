using System.Runtime.InteropServices;

public readonly struct A
{
  private readonly byte _opaque;
}

public readonly struct B
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct List_A
{

  public required A* members;
  public required nuint count;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct List_B
{

  public required B* members;
  public required nuint count;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial void foo(List_A a);

  [LibraryImport("library", EntryPoint = "bar")]
  public unsafe static partial void bar(List_B b);

}
