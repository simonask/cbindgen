using System.Runtime.InteropServices;
using C_F = C_A;

public enum C_E : byte
{
  x = 0,
  y = 1,
};

public readonly struct C_A
{
  private readonly byte _opaque;
}

public readonly struct C_C
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct C_AwesomeB
{

  public required int x;
  public required float y;
}

[StructLayout(LayoutKind.Explicit)]
public  partial struct C_D
{
  [FieldOffset(0)]
  public int x;
  [FieldOffset(0)]
  public float y;
}
public static partial class Api
{

  public const int C_H = (int)10;
  // cbindgen: Skipped 'I' because it contains pointer casts

  // cbindgen: Skipped 'G', because it is a static extern.

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(C_A* a, C_AwesomeB b, C_C c, C_D d, C_E e, C_F f);

}
