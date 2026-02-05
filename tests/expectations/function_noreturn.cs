using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Example
{

  public required delegate* unmanaged[Cdecl]<nuint, nuint, void> f;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "loop_forever")]
  [System.Diagnostics.CodeAnalysis.DoesNotReturn]
  public unsafe static partial void loop_forever();

  [LibraryImport("library", EntryPoint = "normal_return")]
  public unsafe static partial byte
  normal_return(Example arg,
    delegate* unmanaged[Cdecl]<byte, void> other);

}
