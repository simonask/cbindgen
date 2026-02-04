using System.Runtime.InteropServices;

/// <summary>
/// ///  Constants shared by multiple CSS Box Alignment properties
/// /// 
/// ///  These constants match Gecko's `NS_STYLE_ALIGN_*` constants.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public  partial record struct AlignFlags
{

  /// <summary>
  /// ///  'auto'
  /// </summary>
  public static readonly AlignFlags AUTO = new AlignFlags
  {
    bits = (byte)(byte)0,

  };

  /// <summary>
  /// ///  'normal'
  /// </summary>
  public static readonly AlignFlags NORMAL = new AlignFlags
  {
    bits = (byte)(byte)1,

  };

  /// <summary>
  /// ///  'start'
  /// </summary>
  public static readonly AlignFlags START = new AlignFlags
  {
    bits = (byte)(byte)(1 << (int)1),

  };

  /// <summary>
  /// ///  'end'
  /// </summary>
  public static readonly AlignFlags END = new AlignFlags
  {
    bits = (byte)(byte)(1 << (int)2),

  };

  public static readonly AlignFlags ALIAS = new AlignFlags
  {
    bits = (byte)(byte)(AlignFlags.END).bits,

  };

  /// <summary>
  /// ///  'flex-start'
  /// </summary>
  public static readonly AlignFlags FLEX_START = new AlignFlags
  {
    bits = (byte)(byte)(1 << (int)3),

  };

  public static readonly AlignFlags MIXED = new AlignFlags
  {
    bits = (byte)(byte)(((1 << (int)4) | (AlignFlags.FLEX_START).bits) | (AlignFlags.END).bits),

  };

  public static readonly AlignFlags MIXED_SELF = new AlignFlags
  {
    bits = (byte)(byte)(((1 << (int)5) | (AlignFlags.FLEX_START).bits) | (AlignFlags.END).bits),

  };

  public required byte bits;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct DebugFlags
{

  /// <summary>
  /// ///  Flag with the topmost bit set of the u32
  /// </summary>
  public static readonly DebugFlags BIGGEST_ALLOWED = new DebugFlags
  {
    bits = (uint)(uint)(1 << (int)31),

  };

  public required uint bits;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct LargeFlags
{

  /// <summary>
  /// ///  Flag with a very large shift that usually would be narrowed.
  /// </summary>
  public static readonly LargeFlags LARGE_SHIFT = new LargeFlags
  {
    bits = (ulong)(ulong)(1 << (int)44),

  };

  public static readonly LargeFlags INVERTED = new LargeFlags
  {
    bits = (ulong)(ulong)~(LargeFlags.LARGE_SHIFT).bits,

  };

  public required ulong bits;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct OutOfLine
{

  public static readonly OutOfLine A = new OutOfLine
  {
    _0 = (uint)(uint)1,

  };

  public static readonly OutOfLine B = new OutOfLine
  {
    _0 = (uint)(uint)2,

  };

  public static readonly OutOfLine AB = new OutOfLine
  {
    _0 = (uint)(uint)((OutOfLine.A)._0 | (OutOfLine.B)._0),

  };

  public required uint _0;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(AlignFlags flags,
  DebugFlags bigger_flags,
  LargeFlags largest_flags,
  OutOfLine out_of_line);

}
