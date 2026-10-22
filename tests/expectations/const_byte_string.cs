using System.Runtime.InteropServices;
public static partial class Api
{
  public static ReadOnlySpan<byte> BYTES => [97, 98, 99];
  public static ReadOnlySpan<byte> SLICE => [110, 111, 116, 32, 110, 117, 108, 108, 0, 116, 101, 114, 109, 105, 110, 97, 116, 101, 100];
  public static ReadOnlySpan<byte> BINARY => [0, 255, 16, 127];
  public static ReadOnlySpan<byte> EMPTY => [];
  /// <summary>
  ///      A documented byte-string constant.
  /// </summary>
  public static ReadOnlySpan<byte> DOCUMENTED => [100, 111, 99];

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root();

}
