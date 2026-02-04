using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct Foo
{
  public enum Foo_Tag
  {
    A,
  };
  private Foo_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public record struct A_Body()
    {
      public required InlineArray20_float a;
    }
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo a);

}
[System.Runtime.CompilerServices.InlineArray(20)]
public struct InlineArray20_float
{
  private float _data;
}
