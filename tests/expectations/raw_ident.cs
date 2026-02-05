using System.Runtime.InteropServices;

public enum Enum : byte
{
  a,
  b,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Struct
{

  public required Enum @field;
}
public static partial class Api
{

  // cbindgen: Skipped 'STATIC', because it is a static extern.

  [LibraryImport("library", EntryPoint = "fn")]
  public unsafe static partial void fn(Struct arg);

}
