using System.Runtime.InteropServices;

public enum A : byte
{
  A_A1,
  A_A2,
  A_A3,
  /// <summary>
  ///      Must be last for serialization purposes
  /// </summary>
  A_Sentinel,
};

public enum B : byte
{
  B_B1,
  B_B2,
  B_B3,
  /// <summary>
  ///      Must be last for serialization purposes
  /// </summary>
  B_Sentinel,
};

[StructLayout(LayoutKind.Explicit)]
public struct C
{
  public enum Tag : byte
  {
    C_C1,
    C_C2,
    C_C3,
    /// <summary>
    ///      Must be last for serialization purposes
    /// </summary>
    C_Sentinel,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private C_C1_Body c1;
  [FieldOffset(0)]
  private C_C2_Body c2;

  public static C C1(uint a)
     => new(new C_C1_Body
      {
        a = a,

      });
  public static C C2(uint b)
     => new(new C_C2_Body
      {
        b = b,

      });
  public static C C3() => new() { _tag = Tag.C_C3 };
  /// <summary>
  ///      Must be last for serialization purposes
  /// </summary>
  public static C Sentinel() => new() { _tag = Tag.C_Sentinel };

  public C(C_C1_Body c1)
  {
    this.c1 = c1;
  }
  public C(C_C2_Body c2)
  {
    this.c2 = c2;
  }



  public static implicit operator C(C_C1_Body value) => new(value);
  public static implicit operator C(C_C2_Body value) => new(value);

  public readonly bool IsC1 => _tag == Tag.C_C1;
  public readonly bool IsC2 => _tag == Tag.C_C2;
  public readonly bool IsC3 => _tag == Tag.C_C3;
  public readonly bool IsSentinel => _tag == Tag.C_Sentinel;

  public readonly C_C1_Body? AsC1 => _tag == Tag.C_C1 ? c1 : null;
  public readonly C_C2_Body? AsC2 => _tag == Tag.C_C2 ? c2 : null;


  [StructLayout(LayoutKind.Sequential)]
  public record struct C_C1_Body()
  {
    private readonly Tag _tag = Tag.C_C1;
    public required uint a;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct C_C2_Body()
  {
    private readonly Tag _tag = Tag.C_C2;
    public required uint b;
  }


}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A a, B b, C c);

}
