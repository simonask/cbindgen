using System.Runtime.InteropServices;

/// <summary>
/// ///  Specifies which tracks(s) on the axis that the position-area span occupies.
/// ///  Represented as 3 bits: start, center, end track.
/// </summary>
public enum PositionAreaTrack : byte
{
  /// <summary>
  /// ///  First track
  /// </summary>
  Start = 1,
  /// <summary>
  /// ///  First and center.
  /// </summary>
  SpanStart = 3,
  /// <summary>
  /// ///  Last track.
  /// </summary>
  End = 4,
  /// <summary>
  /// ///  Last and center.
  /// </summary>
  SpanEnd = 6,
  /// <summary>
  /// ///  Center track.
  /// </summary>
  Center = 2,
  /// <summary>
  /// ///  All tracks
  /// </summary>
  SpanAll = 7,
};

/// <summary>
/// ///  A three-bit value that represents the axis in which position-area operates on.
/// ///  Represented as 3 bits: axis type (physical or logical), direction type (physical or logical),
/// ///  axis value.
/// </summary>
public enum PositionAreaAxis : byte
{
  Horizontal = 0,
  Vertical = 1,
  X = 2,
  Y = 3,
  Inline = 6,
  Block = 7,
};

/// <summary>
/// ///  Possible values for the `position-area` property's keywords.
/// ///  Represented by [0z xxx yyy], where z means "self wm resolution", xxx is the type (as in
/// ///  PositionAreaAxis and yyy is the PositionAreaTrack
/// ///  https://drafts.csswg.org/css-anchor-position-1/#propdef-position-area
/// </summary>
public enum PositionAreaKeyword : byte
{
  None = 0,
  Center = (byte)PositionAreaTrack.Center,
  SpanAll = (byte)PositionAreaTrack.SpanAll,
  Start = (byte)PositionAreaTrack.Start,
  End = (byte)PositionAreaTrack.End,
  SpanStart = (byte)PositionAreaTrack.SpanStart,
  SpanEnd = (byte)PositionAreaTrack.SpanEnd,
  Top = (((byte)PositionAreaAxis.Vertical << (int)Api.AXIS_SHIFT) | (byte)PositionAreaTrack.Start),
  Bottom = (((byte)PositionAreaAxis.Vertical << (int)Api.AXIS_SHIFT) | (byte)PositionAreaTrack.End),
};
public static partial class Api
{

  public const nuint AXIS_SHIFT = (nuint)3;

  public const nuint SELF_WM_SHIFT = (nuint)6;

  public const byte SELF_WM = (byte)(1 << (int)6);

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(PositionAreaKeyword arg0,
  PositionAreaTrack arg1,
  PositionAreaAxis arg2);

}
