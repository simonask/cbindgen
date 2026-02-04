using System.Runtime.InteropServices;

public readonly struct Opaque
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Foo_u64
{

  public required float* a;
  public required ulong* b;
  public required Opaque* c;
  public required ulong** d;
  public required float** e;
  public required Opaque** f;
  public required ulong* g;
  public required int* h;
  public required int** i;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(int* arg, Foo_u64* foo, Opaque** d);

}
