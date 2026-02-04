using System.Runtime.InteropServices;
using Boo = FooU8;

public enum Bar
{
  BarSome,
  BarThing,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct FooU8
{

  public required byte a;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Boo x, Bar y);

  [LibraryImport("library", EntryPoint = "unsafe_root")]
  public unsafe static partial void unsafe_root(Boo x, Bar y);

}
