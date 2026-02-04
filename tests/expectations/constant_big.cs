using System.Runtime.InteropServices;
public static partial class Api
{

  public const ulong UNSIGNED_NEEDS_ULL_SUFFIX = (ulong)9223372036854775808;

  public const ulong UNSIGNED_DOESNT_NEED_ULL_SUFFIX = (ulong)8070450532247928832;

  public const long SIGNED_NEEDS_ULL_SUFFIX = (long)-9223372036854775808;

  public const long SIGNED_DOESNT_NEED_ULL_SUFFIX = (long)-9223372036854775807;

}
