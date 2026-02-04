using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Foo_Bar
{

  public required int* something;
}

[StructLayout(LayoutKind.Explicit)]
public struct Bar
{
  public enum Bar_Tag : byte
  {
    Min,
    Max,
    Other,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct Min_Body()
  {
    private readonly Bar_Tag _tag = Bar_Tag.Min;
    public required Foo_Bar min;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Max_Body()
  {
    private readonly Bar_Tag _tag = Bar_Tag.Max;
    public required Foo_Bar max;
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Bar b);

}
