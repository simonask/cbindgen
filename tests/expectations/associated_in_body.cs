using System.Runtime.InteropServices;

/// <summary>
/// ///  Constants shared by multiple CSS Box Alignment properties
/// /// 
/// ///  These constants match Gecko's `NS_STYLE_ALIGN_*` constants.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public  partial record struct StyleAlignFlags
{

  /// <summary>
  /// ///  'auto'
  /// </summary>
  public static readonly StyleAlignFlags AUTO = new StyleAlignFlags
  {
    bits = (byte)(byte)0,

  };

  /// <summary>
  /// ///  'normal'
  /// </summary>
  public static readonly StyleAlignFlags NORMAL = new StyleAlignFlags
  {
    bits = (byte)(byte)1,

  };

  /// <summary>
  /// ///  'start'
  /// </summary>
  public static readonly StyleAlignFlags START = new StyleAlignFlags
  {
    bits = (byte)(byte)(1 << (int)1),

  };

  /// <summary>
  /// ///  'end'
  /// </summary>
  public static readonly StyleAlignFlags END = new StyleAlignFlags
  {
    bits = (byte)(byte)(1 << (int)2),

  };

  public static readonly StyleAlignFlags ALIAS = new StyleAlignFlags
  {
    bits = (byte)(byte)(StyleAlignFlags.END).bits,

  };

  /// <summary>
  /// ///  'flex-start'
  /// </summary>
  public static readonly StyleAlignFlags FLEX_START = new StyleAlignFlags
  {
    bits = (byte)(byte)(1 << (int)3),

  };

  public static readonly StyleAlignFlags MIXED = new StyleAlignFlags
  {
    bits = (byte)(byte)(((1 << (int)4) | (StyleAlignFlags.FLEX_START).bits) | (StyleAlignFlags.END).bits),

  };

  public static readonly StyleAlignFlags MIXED_SELF = new StyleAlignFlags
  {
    bits = (byte)(byte)(((1 << (int)5) | (StyleAlignFlags.FLEX_START).bits) | (StyleAlignFlags.END).bits),

  };

#if PLATFORM_WIN
  public static readonly StyleAlignFlags PLATFORM_BIT = new StyleAlignFlags
  {
    bits = (byte)(byte)(1 << (int)6),

  };
#endif

#if PLATFORM_UNIX
  public static readonly StyleAlignFlags PLATFORM_BIT = new StyleAlignFlags
  {
    bits = (byte)(byte)(1 << (int)7),

  };
#endif

  public required byte bits;
}

/// <summary>
/// ///  An arbitrary identifier for a native (OS compositor) surface
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public  partial record struct StyleNativeSurfaceId
{

  /// <summary>
  /// ///  A special id for the native surface that is used for debug / profiler overlays.
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
  /// ///  A special id for the native surface that is used for debug / profiler overlays.
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
