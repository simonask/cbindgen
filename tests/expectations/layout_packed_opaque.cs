using System.Runtime.InteropServices;

public readonly struct OpaquePackedStruct
{
  private readonly byte _opaque;
}

public readonly struct OpaquePackedUnion
{
  private readonly byte _opaque;
}

// cbindgen: Ignoring union Align1Union because it is has alignment attributes

// cbindgen: Ignoring union Align4Union because it is has alignment attributes

// cbindgen: Ignoring union Align16Union because it is has alignment attributes

// cbindgen: Ignoring struct Align1Struct because it is has alignment attributes

// cbindgen: Ignoring struct Align2Struct because it is has alignment attributes

// cbindgen: Ignoring struct Align4Struct because it is has alignment attributes

// cbindgen: Ignoring struct Align8Struct because it is has alignment attributes

// cbindgen: Ignoring struct Align32Struct because it is has alignment attributes
public static partial class Api
{

}
