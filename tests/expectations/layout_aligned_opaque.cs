using System.Runtime.InteropServices;

public readonly struct OpaqueAlign16Union
{
  private readonly byte _opaque;
}

public readonly struct OpaqueAlign1Struct
{
  private readonly byte _opaque;
}

public readonly struct OpaqueAlign1Union
{
  private readonly byte _opaque;
}

public readonly struct OpaqueAlign2Struct
{
  private readonly byte _opaque;
}

public readonly struct OpaqueAlign32Struct
{
  private readonly byte _opaque;
}

public readonly struct OpaqueAlign4Struct
{
  private readonly byte _opaque;
}

public readonly struct OpaqueAlign4Union
{
  private readonly byte _opaque;
}

public readonly struct OpaqueAlign8Struct
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe partial  struct PackedStruct
{

  public required nuint arg1;
  public required byte* arg2;
}

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
