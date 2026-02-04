using System.Runtime.InteropServices;

public readonly struct Opaque
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Explicit)]
public  partial struct Normal
{
  [FieldOffset(0)]
  public int x;
  [FieldOffset(0)]
  public float y;
}

[StructLayout(LayoutKind.Explicit)]
public  partial struct NormalWithZST
{
  [FieldOffset(0)]
  public int x;
  [FieldOffset(0)]
  public float y;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Opaque* a, Normal b, NormalWithZST c);

}
