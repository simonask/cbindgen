using System.Runtime.InteropServices;
using Foo = NotReprC_Point;

public readonly struct NotReprC_Point
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Point
{

  public required int x;
  public required int y;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct MyStruct
{

  public required Point point;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo* a, MyStruct* with_manual_drop);

  [LibraryImport("library", EntryPoint = "take")]
  public unsafe static partial void take(Point with_manual_drop);

}
