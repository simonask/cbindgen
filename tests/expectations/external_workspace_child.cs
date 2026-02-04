using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct ExtType
{

  public required uint data;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "consume_ext")]
  public unsafe static partial void consume_ext(ExtType _ext);

}
