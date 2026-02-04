using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TransparentStruct
{

  public const long TransparentStruct_ASSOC_STRUCT_FOO = (long)1;
  public static readonly TransparentStruct ASSOC_STRUCT_BAR = new TransparentStruct
  {
    field = (byte)2,

  };


  public required byte field;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TransparentTupleStruct
{

  public required byte _0;
}
public static partial class Api
{
  public static readonly TransparentStruct STRUCT_FOO = new TransparentStruct
  {
    field = (byte)4,

  };
  public static readonly TransparentTupleStruct STRUCT_BAR = new TransparentTupleStruct
  {
    _0 = (byte)5,

  };

}
