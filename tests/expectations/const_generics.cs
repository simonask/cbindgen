using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct CArrayString_TITLE_SIZE
{

  public required InlineArray_TITLE_SIZE_sbyte chars;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct CArrayString_40
{

  public required InlineArray40_sbyte chars;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Book
{

  public required CArrayString_TITLE_SIZE title;
  public required CArrayString_40 author;
}
public static partial class Api
{

  public const nuint TITLE_SIZE = (nuint)80;

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Book* a);

}
[System.Runtime.CompilerServices.InlineArray(40)]
public struct InlineArray40_sbyte
{
  private sbyte _data;
}
[System.Runtime.CompilerServices.InlineArray((int)Api.TITLE_SIZE)]
public struct InlineArray_TITLE_SIZE_sbyte
{
  private sbyte _data;
}
