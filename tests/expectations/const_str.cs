using System.Runtime.InteropServices;
public static partial class Api
{
  public static ReadOnlySpan<byte> STRING => "string"u8;
  public static ReadOnlySpan<byte> C_STRING => "c string"u8;
  public static ReadOnlySpan<byte> EMPTY => ""u8;
  public static ReadOnlySpan<byte> WITH_QUOTES => "a \"quoted\" word"u8;
  public static ReadOnlySpan<byte> WITH_BACKSLASH => "back\\slash"u8;
  public static ReadOnlySpan<byte> WITH_CONTROLS => "tab\tnewline\nreturn\r"u8;
  public static ReadOnlySpan<byte> OTHER_CONTROLS => "\u0001\u000B\u000C\u007F"u8;
  public static ReadOnlySpan<byte> TRIGRAPHS => "what??!"u8;
  public static ReadOnlySpan<byte> INTERIOR_NUL => "a\0b"u8;
  public static ReadOnlySpan<byte> NON_UTF8 => [0xFF, 0xFE];
  public static ReadOnlySpan<byte> NON_ASCII => "caf\u00E9"u8;
  public static ReadOnlySpan<byte> HEX_ADJACENT => [0xC3, 0x61];
  public static ReadOnlySpan<byte> HEX_ADJACENT_UPPER => [0xC3, 0x46];
  public static ReadOnlySpan<byte> HEX_NON_ADJACENT => [0xC3, 0x7A];
  // cbindgen: Skipped 'ARRAY' because its element type cannot be represented in a C# constant
  /// <summary>
  ///      A documented string constant.
  /// </summary>
  public static ReadOnlySpan<byte> DOCUMENTED => "documented"u8;

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root();

}
