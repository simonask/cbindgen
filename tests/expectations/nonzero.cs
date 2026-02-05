using System.Runtime.InteropServices;

public readonly struct Option_i64
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct NonZeroAliases
{

  public required byte a;
  public required ushort b;
  public required uint c;
  public required ulong d;
  public required sbyte e;
  public required short f;
  public required int g;
  public required long h;
  public required long i;
  public required Option_i64* j;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct NonZeroGenerics
{

  public required byte a;
  public required ushort b;
  public required uint c;
  public required ulong d;
  public required sbyte e;
  public required short f;
  public required int g;
  public required long h;
  public required long i;
  public required Option_i64* j;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root_nonzero_aliases")]
  public unsafe static partial void
  root_nonzero_aliases(NonZeroAliases test,
    byte a,
    ushort b,
    uint c,
    ulong d,
    sbyte e,
    short f,
    int g,
    long h,
    long i,
    Option_i64* j);

  [LibraryImport("library", EntryPoint = "root_nonzero_generics")]
  public unsafe static partial void
  root_nonzero_generics(NonZeroGenerics test,
    byte a,
    ushort b,
    uint c,
    ulong d,
    sbyte e,
    short f,
    int g,
    long h,
    long i,
    Option_i64* j);

}
