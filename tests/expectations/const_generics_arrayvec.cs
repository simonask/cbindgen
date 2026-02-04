using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct ArrayVec_____u8__100
{

  public required InlineArray100__ptr__byte xs;
  public required uint len;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "push")]
  public unsafe static partial int push(ArrayVec_____u8__100* v, byte* elem);

}
[System.Runtime.CompilerServices.InlineArray(100)]
public struct InlineArray100__ptr__byte
{
  private nint _data;
}
