using System.Runtime.InteropServices;
using A = nint;
using B = nint;
using C = nint;
using D = nint;
using E = nint;
using F = nint;
using G = nint;
using H = nint;
using I = nint;
using J = nint;
using K = System.Runtime.CompilerServices.InlineArray16<int>;
using L = System.Runtime.CompilerServices.InlineArray16<nint>;
using M = System.Runtime.CompilerServices.InlineArray16<nint>;
using N = System.Runtime.CompilerServices.InlineArray16<nint>;
using P = nint;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "O")]
  public unsafe static partial delegate* unmanaged[Cdecl]<void> O();

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(A a,
    B b,
    C c,
    D d,
    E e,
    F f,
    G g,
    H h,
    I i,
    J j,
    K k,
    L l,
    M m,
    N n,
    P p);

}
