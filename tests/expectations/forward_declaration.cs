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
  public enum TypeData_Tag
  {
    Primitive,
    Struct,
  };
  private TypeData_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {


    [StructLayout(LayoutKind.Sequential)]
    public record struct Struct_Body()
    {
      public required StructInfo struct_;
    }
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
