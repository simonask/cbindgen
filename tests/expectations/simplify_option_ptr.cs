using System.Runtime.InteropServices;

public readonly struct Opaque
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct Option_____Opaque
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Foo
{

  public required Opaque* x;
  public required Opaque* y;
  public required delegate* unmanaged[Cdecl]<void> z;
  public required delegate* unmanaged[Cdecl]<void>* zz;
}

[StructLayout(LayoutKind.Explicit)]
public unsafe partial struct Bar
{
  [FieldOffset(0)]
  public Opaque* x;
  [FieldOffset(0)]
  public Opaque* y;
  [FieldOffset(0)]
  public delegate* unmanaged[Cdecl]<void> z;
  [FieldOffset(0)]
  public delegate* unmanaged[Cdecl]<void>* zz;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(Opaque* a,
    Opaque* b,
    Foo c,
    Bar d,
    Option_____Opaque* e,
    delegate* unmanaged[Cdecl]<Opaque*, void> f);

}
