using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Fns
{

  public required delegate* unmanaged[Cdecl]<void> noArgs;
  public required delegate* unmanaged[Cdecl]<int, void> anonymousArg;
  public required delegate* unmanaged[Cdecl]<int> returnsNumber;
  public required delegate* unmanaged[Cdecl]<int, short, sbyte> namedArgs;
  public required delegate* unmanaged[Cdecl]<int, short, long, sbyte> namedArgsWildcards;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Fns _fns);

  [LibraryImport("library", EntryPoint = "no_return")]
  [System.Diagnostics.CodeAnalysis.DoesNotReturn]
  public unsafe static partial void no_return();

}
