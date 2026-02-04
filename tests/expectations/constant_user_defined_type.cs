using System.Runtime.InteropServices;
using A = byte;

public enum E
{
  V,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct S
{

  public required byte field;
}
public static partial class Api
{
  public static readonly S C1 = new S
  {
    field = (byte)0,

  };
  public static readonly E C2 = Api.V;
  public static readonly A C3 = 0;
  public const E V = E.V;
}
