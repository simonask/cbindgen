using System.Runtime.InteropServices;

/// <summary>
///      Constants shared by multiple CSS Box Alignment properties
///     
///      These constants match Gecko's `NS_STYLE_ALIGN_*` constants.
/// </summary>
[Flags]
public enum AlignFlags : byte
{
  /// <summary>
  ///      'auto'
  /// </summary>
  AUTO = unchecked((byte)0),
  /// <summary>
  ///      'normal'
  /// </summary>
  NORMAL = unchecked((byte)1),
  /// <summary>
  ///      'start'
  /// </summary>
  START = unchecked((byte)(1 << (int)1)),
  /// <summary>
  ///      'end'
  /// </summary>
  END = unchecked((byte)(1 << (int)2)),
  ALIAS = unchecked((byte)AlignFlags.END),
  /// <summary>
  ///      'flex-start'
  /// </summary>
  FLEX_START = unchecked((byte)(1 << (int)3)),
  MIXED = unchecked((byte)(((1 << (int)4) | AlignFlags.FLEX_START) | AlignFlags.END)),
  MIXED_SELF = unchecked((byte)(((1 << (int)5) | AlignFlags.FLEX_START) | AlignFlags.END)),
}


[Flags]
public enum DebugFlags : uint
{
  /// <summary>
  ///      Flag with the topmost bit set of the u32
  /// </summary>
  BIGGEST_ALLOWED = unchecked((uint)(1 << (int)31)),
}


[Flags]
public enum LargeFlags : ulong
{
  /// <summary>
  ///      Flag with a very large shift that usually would be narrowed.
  /// </summary>
  LARGE_SHIFT = unchecked((ulong)(1 << (int)44)),
  INVERTED = unchecked((ulong)~LargeFlags.LARGE_SHIFT),
}


[Flags]
public enum OutOfLine : uint
{
  A = unchecked((uint)1),
  B = unchecked((uint)2),
  AB = unchecked((uint)(OutOfLine.A | OutOfLine.B)),
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
