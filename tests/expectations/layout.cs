using System.Runtime.InteropServices;

public readonly struct RustAlign4Struct
{
  private readonly byte _opaque;
}

public readonly struct RustAlign4Union
{
  private readonly byte _opaque;
}

public readonly struct RustPackedStruct
{
  private readonly byte _opaque;
}

public readonly struct RustPackedUnion
{
  private readonly byte _opaque;
}

public readonly struct UnsupportedAlign4Enum
{
  private readonly byte _opaque;
}

public readonly struct UnsupportedPacked4Struct
{
  private readonly byte _opaque;
}

public readonly struct UnsupportedPacked4Union
{
  private readonly byte _opaque;
}

// cbindgen: Ignoring struct Align1Struct because it is has alignment attributes

// cbindgen: Ignoring struct Align2Struct because it is has alignment attributes

// cbindgen: Ignoring struct Align4Struct because it is has alignment attributes

// cbindgen: Ignoring struct Align8Struct because it is has alignment attributes

// cbindgen: Ignoring struct Align32Struct because it is has alignment attributes

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe partial  struct PackedStruct
{

  public required nuint arg1;
  public required byte* arg2;
}

// cbindgen: Ignoring union Align1Union because it is has alignment attributes

// cbindgen: Ignoring union Align4Union because it is has alignment attributes

// cbindgen: Ignoring union Align16Union because it is has alignment attributes

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public unsafe partial struct PackedUnion
{
  [FieldOffset(0)]
  public nuint variant1;
  [FieldOffset(0)]
  public byte* variant2;
}
public static partial class Api
{

}
