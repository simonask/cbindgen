using System.Runtime.InteropServices;

public readonly struct Opaque
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Normal
{

  public required int x;
  public required float y;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct NormalWithZST
{

  public required int x;
  public required float y;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TupleRenamed
{

  public required int m0;
  public required float m1;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TupleNamed
{

  public required int x;
  public required float y;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(Opaque* a,
    Normal b,
    NormalWithZST c,
    TupleRenamed d,
    TupleNamed e);

}
