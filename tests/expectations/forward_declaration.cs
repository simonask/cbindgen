using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct StructInfo
{

  public required TypeInfo** fields;
  public required nuint num_fields;
}

[StructLayout(LayoutKind.Sequential)]
public struct TypeData
{
  public enum Tag
  {
    Primitive,
    Struct,
  };
  private Tag _tag;
  private TypeData_Data _data;

  public static TypeData Primitive() => new() { _tag = Tag.Primitive };
  public static TypeData Struct(StructInfo @struct)
     => new(new Struct_Body
      {
        @struct = @struct,

      });


  public TypeData(Struct_Body @struct)
  {
    _tag = Tag.Struct;
    _data.@struct = @struct;
  }

  public static implicit operator TypeData(Struct_Body value) => new(value);

  public readonly bool IsPrimitive => _tag == Tag.Primitive;
  public readonly bool IsStruct => _tag == Tag.Struct;

  public readonly Struct_Body? AsStruct => _tag == Tag.Struct ? _data.@struct : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct TypeData_Data
  {
    [FieldOffset(0)]
    public Struct_Body @struct;
  }

  [StructLayout(LayoutKind.Sequential)]
  public record struct Struct_Body()
  {
    public required StructInfo @struct;
  }

}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypeInfo
{

  public required TypeData data;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(TypeInfo x);

}
