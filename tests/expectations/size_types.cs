using System.Runtime.InteropServices;
using Usize = nuint;
using Isize = nint;

[StructLayout(LayoutKind.Sequential)]
public readonly record struct UE(nuint RawValue)
{
  public static readonly UE UV = new(0);

}

[StructLayout(LayoutKind.Sequential)]
public readonly record struct IE(nint RawValue)
{
  public static readonly IE IV = new(0);

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Usize arg0, Isize arg1, UE arg2, IE arg3);

}
