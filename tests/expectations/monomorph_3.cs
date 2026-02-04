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

[StructLayout(LayoutKind.Explicit)]
public unsafe partial struct Foo_i32
{
  [FieldOffset(0)]
  public int* data;
}

[StructLayout(LayoutKind.Explicit)]
public unsafe partial struct Foo_f32
{
  [FieldOffset(0)]
  public float* data;
}

[StructLayout(LayoutKind.Explicit)]
public unsafe partial struct Foo_Bar_f32
{
  [FieldOffset(0)]
  public Bar_f32* data;
}

[StructLayout(LayoutKind.Explicit)]
public unsafe partial struct Tuple_Foo_f32_____f32
{
  [FieldOffset(0)]
  public Foo_f32* a;
  [FieldOffset(0)]
  public float* b;
}

[StructLayout(LayoutKind.Explicit)]
public unsafe partial struct Tuple_f32__f32
{
  [FieldOffset(0)]
  public float* a;
  [FieldOffset(0)]
  public float* b;
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
