using System.Runtime.InteropServices;
using PREFIX_NamedLenArray = InlineArray_PREFIX_LEN_int;
using PREFIX_ValuedLenArray = InlineArray22_int;

[StructLayout(LayoutKind.Explicit)]
public struct PREFIX_AbsoluteFontWeight
{
  public enum Tag : byte
  {
    Weight,
    Normal,
    Bold,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private PREFIX_Weight_Body weight;

  public static PREFIX_AbsoluteFontWeight Weight(float weight)
     => new(new PREFIX_Weight_Body
      {
        weight = weight,

      });
  public static PREFIX_AbsoluteFontWeight Normal() => new() { _tag = Tag.Normal };
  public static PREFIX_AbsoluteFontWeight Bold() => new() { _tag = Tag.Bold };

  public PREFIX_AbsoluteFontWeight(PREFIX_Weight_Body weight)
  {
    this.weight = weight;
  }



  public static implicit operator PREFIX_AbsoluteFontWeight(PREFIX_Weight_Body value) => new(value);

  public readonly bool IsWeight => _tag == Tag.Weight;
  public readonly bool IsNormal => _tag == Tag.Normal;
  public readonly bool IsBold => _tag == Tag.Bold;

  public readonly PREFIX_Weight_Body? AsWeight => _tag == Tag.Weight ? weight : null;


  [StructLayout(LayoutKind.Sequential)]
  public record struct PREFIX_Weight_Body()
  {
    private readonly Tag _tag = Tag.Weight;
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
