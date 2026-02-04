using System.Runtime.InteropServices;
using PREFIX_NamedLenArray = InlineArray_PREFIX_LEN_int;
using PREFIX_ValuedLenArray = InlineArray22_int;

[StructLayout(LayoutKind.Explicit)]
public struct PREFIX_AbsoluteFontWeight
{
  public enum PREFIX_AbsoluteFontWeight_Tag : byte
  {
    Weight,
    Normal,
    Bold,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct PREFIX_Weight_Body()
  {
    private readonly PREFIX_AbsoluteFontWeight_Tag _tag = PREFIX_AbsoluteFontWeight_Tag.Weight;
    public required float weight;
  }


}
public static partial class Api
{

  public const int PREFIX_LEN = (int)22;

  public const long PREFIX_X = (long)(22 << (int)22);

  public const long PREFIX_Y = (long)(Api.PREFIX_X + Api.PREFIX_X);

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(PREFIX_NamedLenArray x,
  PREFIX_ValuedLenArray y,
  PREFIX_AbsoluteFontWeight z);

}
[System.Runtime.CompilerServices.InlineArray(22)]
public struct InlineArray22_int
{
  private int _data;
}
[System.Runtime.CompilerServices.InlineArray((int)Api.PREFIX_LEN)]
public struct InlineArray_PREFIX_LEN_int
{
  private int _data;
}
