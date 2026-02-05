using System.Runtime.InteropServices;

/// <summary>
///      Constants shared by multiple CSS Box Alignment properties
///     
///      These constants match Gecko's `NS_STYLE_ALIGN_*` constants.
/// </summary>
[Flags]
public enum StyleAlignFlags : byte
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
  ALIAS = unchecked((byte)StyleAlignFlags.END),
  /// <summary>
  ///      'flex-start'
  /// </summary>
  FLEX_START = unchecked((byte)(1 << (int)3)),
  MIXED = unchecked((byte)(((1 << (int)4) | StyleAlignFlags.FLEX_START) | StyleAlignFlags.END)),
  MIXED_SELF = unchecked((byte)(((1 << (int)5) | StyleAlignFlags.FLEX_START) | StyleAlignFlags.END)),
#if PLATFORM_WIN
  PLATFORM_BIT = unchecked((byte)(1 << (int)6)),
#endif
#if PLATFORM_UNIX
  PLATFORM_BIT = unchecked((byte)(1 << (int)7)),
#endif
}


/// <summary>
///      An arbitrary identifier for a native (OS compositor) surface
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public  partial record struct StyleNativeSurfaceId
{

  /// <summary>
  ///      A special id for the native surface that is used for debug / profiler overlays.
  /// </summary>
  public static readonly StyleNativeSurfaceId DEBUG_OVERLAY = new StyleNativeSurfaceId
  {
    _0 = (ulong)ulong.MaxValue,

  };

  public required ulong _0;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct StyleNativeTileId
{

  /// <summary>
  ///      A special id for the native surface that is used for debug / profiler overlays.
  /// </summary>
  public static readonly StyleNativeTileId DEBUG_OVERLAY = new StyleNativeTileId
  {
    surface_id = (StyleNativeSurfaceId)StyleNativeSurfaceId.DEBUG_OVERLAY,
    x = (int)0,
    y = (int)0,

  };

  public required StyleNativeSurfaceId surface_id;
  public required int x;
  public required int y;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(StyleAlignFlags flags, StyleNativeTileId tile);

}
