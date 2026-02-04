using System.Runtime.InteropServices;

public readonly struct Opaque
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct References
{

  public required Opaque* a;
  public required Opaque* b;
  public required Opaque* c;
  public required Opaque* d;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Pointers_u64
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
  public required ulong* j;
  public required ulong* k;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "value_arg")]
  public unsafe static partial void value_arg(References arg);

  [LibraryImport("library", EntryPoint = "mutltiple_args")]
  public unsafe static partial void mutltiple_args(int* arg, Pointers_u64* foo, Opaque** d);

  [LibraryImport("library", EntryPoint = "ref_arg")]
  public unsafe static partial void ref_arg(Pointers_u64* arg);

  [LibraryImport("library", EntryPoint = "mut_ref_arg")]
  public unsafe static partial void mut_ref_arg(Pointers_u64* arg);

  [LibraryImport("library", EntryPoint = "optional_ref_arg")]
  public unsafe static partial void optional_ref_arg(Pointers_u64* arg);

  [LibraryImport("library", EntryPoint = "optional_mut_ref_arg")]
  public unsafe static partial void optional_mut_ref_arg(Pointers_u64* arg);

  [LibraryImport("library", EntryPoint = "nullable_const_ptr")]
  public unsafe static partial void nullable_const_ptr(Pointers_u64* arg);

  [LibraryImport("library", EntryPoint = "nullable_mut_ptr")]
  public unsafe static partial void nullable_mut_ptr(Pointers_u64* arg);

}
