using System.Runtime.InteropServices;

public readonly struct OpaqueAlign16Union
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct OpaqueAlign1Struct
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct OpaqueAlign1Union
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct OpaqueAlign2Struct
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct OpaqueAlign32Struct
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct OpaqueAlign4Struct
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct OpaqueAlign4Union
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct OpaqueAlign8Struct
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
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
