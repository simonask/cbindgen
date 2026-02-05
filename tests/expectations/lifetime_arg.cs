using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct A
{

  public required int* data;
}

[StructLayout(LayoutKind.Sequential)]
public struct E
{
  public enum Tag
  {
    V,
    U,
  };
  private Tag _tag;
  private E_Data _data;

  public static E V() => new() { _tag = Tag.V };
  public unsafe static E U(byte* u)
     => new(new U_Body
      {
        u = u,

      });


  public E(U_Body u)
  {
    _tag = Tag.U;
    _data.u = u;
  }

  public static implicit operator E(U_Body value) => new(value);

  public readonly bool IsV => _tag == Tag.V;
  public readonly bool IsU => _tag == Tag.U;

  public readonly U_Body? AsU => _tag == Tag.U ? _data.u : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct E_Data
  {
    [FieldOffset(0)]
    public U_Body u;
  }

  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct U_Body()
  {
    public required byte* u;
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A _a, E _e);

}
