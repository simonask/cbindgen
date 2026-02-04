using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct A
{

  public required int namespace_;
  public required float float_;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct B
{

  public required int namespace_;
  public required float float_;
}

[StructLayout(LayoutKind.Sequential)]
public struct C
{
  public enum C_Tag : byte
  {
    D,
  };
  private C_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public record struct D_Body()
    {
      public required int namespace_;
      public required float float_;
    }
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct E
{
  public enum E_Tag : byte
  {
    Double,
    Float,
  };
  private E_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public record struct Double_Body()
    {
      public required double double_;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct Float_Body()
    {
      public required float float_;
    }
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct F
{
  public enum F_Tag : byte
  {
    double_,
    float_,
  };
  private F_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public record struct double_Body()
    {
      public required double double_;
    }
    [StructLayout(LayoutKind.Sequential)]
    public record struct float_Body()
    {
      public required float float_;
    }
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A a, B b, C c, E e, F f, int namespace_, float float_);

}
