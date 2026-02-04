using System.Runtime.InteropServices;
using Indirection_f32 = Tuple_f32__f32;

public readonly struct Bar_Bar_f32
{
  private readonly byte _opaque;
}

public readonly struct Bar_Foo_f32
{
  private readonly byte _opaque;
}

public readonly struct Bar_f32
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Foo_i32
{

  public required int* data;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Foo_f32
{

  public required float* data;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Foo_Bar_f32
{

  public required Bar_f32* data;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Tuple_Foo_f32_____f32
{

  public required Foo_f32* a;
  public required float* b;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Tuple_f32__f32
{

  public required float* a;
  public required float* b;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(Foo_i32 a,
  Foo_f32 b,
  Bar_f32 c,
  Foo_Bar_f32 d,
  Bar_Foo_f32 e,
  Bar_Bar_f32 f,
  Tuple_Foo_f32_____f32 g,
  Indirection_f32 h);

}
