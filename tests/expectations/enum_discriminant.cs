using System.Runtime.InteropServices;

public enum E : sbyte
{
  A = 1,
  B = -1,
  C = (1 + 2),
  D = Api.FOURTY_FOUR,
  F = 5,
  G = unchecked((sbyte)54),
  H = unchecked((sbyte)0),
};
public static partial class Api
{

  public const sbyte FOURTY_FOUR = (sbyte)4;

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(E* arg0);

}
