using System.Runtime.InteropServices;

public readonly struct DummyStruct
{
  private readonly byte _opaque;
}

public readonly struct EnumWithAssociatedConstantInImpl
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TransparentComplexWrappingStructTuple
{

  public required DummyStruct _0;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TransparentPrimitiveWrappingStructTuple
{

  public required uint _0;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TransparentComplexWrappingStructure
{

  public required DummyStruct only_field;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TransparentPrimitiveWrappingStructure
{

  public required uint only_field;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TransparentComplexWrapper_i32
{

  public required DummyStruct only_non_zero_sized_field;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TransparentPrimitiveWrapper_i32
{

  public required uint only_non_zero_sized_field;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TransparentPrimitiveWithAssociatedConstants
{

  public static readonly TransparentPrimitiveWithAssociatedConstants ZERO = new TransparentPrimitiveWithAssociatedConstants
  {
    bits = (uint)0,

  };

  public static readonly TransparentPrimitiveWithAssociatedConstants ONE = new TransparentPrimitiveWithAssociatedConstants
  {
    bits = (uint)1,

  };

  public required uint bits;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TransparentEmptyStructure
{


}
public static partial class Api
{
  public static readonly TransparentPrimitiveWrappingStructure TEN = new TransparentPrimitiveWrappingStructure
  {
    only_field = (uint)10,

  };

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(TransparentComplexWrappingStructTuple a,
  TransparentPrimitiveWrappingStructTuple b,
  TransparentComplexWrappingStructure c,
  TransparentPrimitiveWrappingStructure d,
  TransparentComplexWrapper_i32 e,
  TransparentPrimitiveWrapper_i32 f,
  TransparentPrimitiveWithAssociatedConstants g,
  TransparentEmptyStructure h,
  EnumWithAssociatedConstantInImpl i);

}
