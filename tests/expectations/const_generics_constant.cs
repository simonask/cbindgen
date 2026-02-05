using System.Runtime.InteropServices;
using FontWeightFixedPoint = FixedPoint_FONT_WEIGHT_FRACTION_BITS;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct FixedPoint_FONT_WEIGHT_FRACTION_BITS
{

  public required ushort @value;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct FontWeight
{

  public static readonly FontWeight NORMAL = new FontWeight
  {
    _0 = (FontWeightFixedPoint)new FontWeightFixedPoint
    {
      @value = (ushort)(400 << (int)Api.FONT_WEIGHT_FRACTION_BITS),

    },

  };

  public required FontWeightFixedPoint _0;
}
public static partial class Api
{

  public const ushort FONT_WEIGHT_FRACTION_BITS = (ushort)6;

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(FontWeight w);

}
