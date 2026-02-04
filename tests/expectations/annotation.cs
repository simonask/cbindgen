using System.Runtime.InteropServices;

public enum C : uint
{
  X = 2,
  Y,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct A
{

  public required int m0;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct B
{

  public required int x;
  public required float y;
}

[StructLayout(LayoutKind.Explicit)]
public struct F
{
  public enum F_Tag : byte
  {
    Foo,
    Bar,
    Baz,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct Foo_Body()
  {
    private readonly F_Tag _tag = F_Tag.Foo;
    public required short foo;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar_Body()
  {
    private readonly F_Tag _tag = F_Tag.Bar;
    public required byte x;
    public required short y;
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct H
{
  public enum H_Tag : byte
  {
    Hello,
    There,
    Everyone,
  };
  private H_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public record struct Hello_Body()
    {
      public required short hello;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct There_Body()
    {
      public required byte x;
      public required short y;
    }

  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A x, B y, C z, F f, H h);

}
