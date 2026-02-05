using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{

  public required InlineArray_FOO_int x;
}
public static partial class Api
{

  public const int FOO = (int)10;

  public const uint DELIMITER = (uint)':';

  public const uint LEFTCURLY = (uint)'{';

  public const uint QUOTE = (uint)'\'';

  public const uint TAB = (uint)'\t';

  public const uint NEWLINE = (uint)'\n';

  public const uint HEART = (uint)0x00002764;

  public const uint EQUID = (uint)0x00010083;

  public const float ZOM = (float)3.14;

  /// <summary>
  ///      A single-line doc comment.
  /// </summary>
  public const sbyte POS_ONE = (sbyte)1;

  /// <summary>
  ///      A
  ///      multi-line
  ///      doc
  ///      comment.
  /// </summary>
  public const sbyte NEG_ONE = (sbyte)-1;

  public const long SHIFT = (long)3;

  public const long XBOOL = (long)1;

  public const long XFALSE = (long)((0 << (int)Api.SHIFT) | Api.XBOOL);

  public const long XTRUE = (long)(1 << (int)(Api.SHIFT | Api.XBOOL));

  public const byte CAST = (byte)unchecked((byte)'A');

  public const uint DOUBLE_CAST = (uint)unchecked((uint)unchecked((float)1));

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo x);

}
[System.Runtime.CompilerServices.InlineArray((int)Api.FOO)]
public struct InlineArray_FOO_int
{
  private int _data;
}
