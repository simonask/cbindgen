using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{

  public required bool a { readonly get => _a != 0; set => _a = value ? (byte)1 : (byte)0; }
  private byte _a;
  public required int b;
}

[StructLayout(LayoutKind.Explicit)]
public struct Bar
{
  public enum Bar_Tag : byte
  {
    Baz,
    Bazz,
    FooNamed,
    FooParen,
  };


  [StructLayout(LayoutKind.Sequential)]
  public record struct Bazz_Body()
  {
    private readonly Bar_Tag _tag = Bar_Tag.Bazz;
    public required Foo named;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct FooNamed_Body()
  {
    private readonly Bar_Tag _tag = Bar_Tag.FooNamed;
    public required int different;
    public required uint fields;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct FooParen_Body()
  {
    private readonly Bar_Tag _tag = Bar_Tag.FooParen;
    public required int _0;
    public required Foo _1;
  }
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial Foo root(Bar aBar);

}
