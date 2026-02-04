using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct WithoutAs
{

  public required InlineArray_SIZE_uint items;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct WithAs
{

  public required InlineArray_SIZE_uint items;
}
public static partial class Api
{

  public const nint SIZE = (nint)4;

  [LibraryImport("library", EntryPoint = "some_fn")]
  public unsafe static partial void some_fn(WithoutAs a, WithAs b);

}
[System.Runtime.CompilerServices.InlineArray((int)Api.SIZE)]
public struct InlineArray_SIZE_uint
{
  private uint _data;
}
