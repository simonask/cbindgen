using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct ExtType
{

  public required uint data;
}
public static partial class Api
{

  public const int EXT_CONST = (int)0;

  [LibraryImport("library", EntryPoint = "consume_ext")]
  public unsafe static partial void consume_ext(ExtType _ext);

}
