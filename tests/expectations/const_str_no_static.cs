using System.Runtime.InteropServices;
public static partial class Api
{
  public static ReadOnlySpan<byte> C_STRING => "winner"u8;
  public static ReadOnlySpan<byte> BYTES => [97, 98, 99];
  // cbindgen: Skipped 'ARRAY' because its element type cannot be represented in a C# constant

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root();

}
