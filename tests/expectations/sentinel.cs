using System.Runtime.InteropServices;

public enum A : byte
{
  A_A1,
  A_A2,
  A_A3,
  /// <summary>
  /// ///  Must be last for serialization purposes
  /// </summary>
  A_Sentinel,
};

public enum B : byte
{
  B_B1,
  B_B2,
  B_B3,
  /// <summary>
  /// ///  Must be last for serialization purposes
  /// </summary>
  B_Sentinel,
};

[StructLayout(LayoutKind.Explicit)]
public struct C
{
  public enum C_Tag : byte
  {
    C_C1,
    C_C2,
    C_C3,
    /// <summary>
    /// ///  Must be last for serialization purposes
    /// </summary>
    C_Sentinel,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct C_C1_Body()
  {
    private readonly C_Tag _tag = C_Tag.C_C1;
    public required uint a;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct C_C2_Body()
  {
    private readonly C_Tag _tag = C_Tag.C_C2;
    public required uint b;
  }


}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A a, B b, C c);

}
