using System.Runtime.InteropServices;

public readonly struct I
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public struct H
{
  public enum H_Tag : byte
  {
    H_Foo,
    H_Bar,
    H_Baz,
  };
  private H_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public record struct H_Foo_Body()
    {
      public required short foo;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct H_Bar_Body()
    {
      public required byte x;
      public required short y;
    }

  }

}

[StructLayout(LayoutKind.Sequential)]
public struct J
{
  public enum J_Tag : byte
  {
    J_Foo,
    J_Bar,
    J_Baz,
  };
  private J_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public record struct J_Foo_Body()
    {
      public required short foo;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct J_Bar_Body()
    {
      public required byte x;
      public required short y;
    }

  }

}

[StructLayout(LayoutKind.Explicit)]
public struct K
{
  public enum K_Tag : byte
  {
    K_Foo,
    K_Bar,
    K_Baz,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct K_Foo_Body()
  {
    private readonly K_Tag _tag = K_Tag.K_Foo;
    public required short foo;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct K_Bar_Body()
  {
    private readonly K_Tag _tag = K_Tag.K_Bar;
    public required byte x;
    public required short y;
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial void foo(H h, I i, J j, K k);

}
