using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct Foo
{
  public enum Tag
  {
    A,
  };
  private Tag _tag;
  private Foo_Data _data;

  public static Foo A(InlineArray20_float a)
     => new(new A_Body
      {
        a = a,

      });

  public Foo(A_Body a)
  {
    _tag = Tag.A;
    _data.a = a;
  }

  public static implicit operator Foo(A_Body value) => new(value);

  public readonly bool IsA => _tag == Tag.A;

  public readonly A_Body? AsA => _tag == Tag.A ? _data.a : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct Foo_Data
  {
    [FieldOffset(0)]
    public A_Body a;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct A_Body()
  {
    public required InlineArray20_float a;
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
