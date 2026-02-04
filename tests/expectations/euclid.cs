using System.Runtime.InteropServices;
using Length_f32 = TypedLength_f32__UnknownUnit;
using LayoutLength = TypedLength_f32__LayoutUnit;
using SideOffsets2D_f32 = TypedSideOffsets2D_f32__UnknownUnit;
using LayoutSideOffsets2D = TypedSideOffsets2D_f32__LayoutUnit;
using Size2D_f32 = TypedSize2D_f32__UnknownUnit;
using LayoutSize2D = TypedSize2D_f32__LayoutUnit;
using Point2D_f32 = TypedPoint2D_f32__UnknownUnit;
using LayoutPoint2D = TypedPoint2D_f32__LayoutUnit;
using Rect_f32 = TypedRect_f32__UnknownUnit;
using LayoutRect = TypedRect_f32__LayoutUnit;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedLength_f32__UnknownUnit
{

  public required float _0;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedLength_f32__LayoutUnit
{

  public required float _0;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedSideOffsets2D_f32__UnknownUnit
{

  public required float top;
  public required float right;
  public required float bottom;
  public required float left;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedSideOffsets2D_f32__LayoutUnit
{

  public required float top;
  public required float right;
  public required float bottom;
  public required float left;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedSize2D_f32__UnknownUnit
{

  public required float width;
  public required float height;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedSize2D_f32__LayoutUnit
{

  public required float width;
  public required float height;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedPoint2D_f32__UnknownUnit
{

  public required float x;
  public required float y;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedPoint2D_f32__LayoutUnit
{

  public required float x;
  public required float y;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedRect_f32__UnknownUnit
{

  public required TypedPoint2D_f32__UnknownUnit origin;
  public required TypedSize2D_f32__UnknownUnit size;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedRect_f32__LayoutUnit
{

  public required TypedPoint2D_f32__LayoutUnit origin;
  public required TypedSize2D_f32__LayoutUnit size;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedTransform2D_f32__UnknownUnit__LayoutUnit
{

  public required float m11;
  public required float m12;
  public required float m21;
  public required float m22;
  public required float m31;
  public required float m32;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct TypedTransform2D_f32__LayoutUnit__UnknownUnit
{

  public required float m11;
  public required float m12;
  public required float m21;
  public required float m22;
  public required float m31;
  public required float m32;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void
  root(TypedLength_f32__UnknownUnit length_a,
  TypedLength_f32__LayoutUnit length_b,
  Length_f32 length_c,
  LayoutLength length_d,
  TypedSideOffsets2D_f32__UnknownUnit side_offsets_a,
  TypedSideOffsets2D_f32__LayoutUnit side_offsets_b,
  SideOffsets2D_f32 side_offsets_c,
  LayoutSideOffsets2D side_offsets_d,
  TypedSize2D_f32__UnknownUnit size_a,
  TypedSize2D_f32__LayoutUnit size_b,
  Size2D_f32 size_c,
  LayoutSize2D size_d,
  TypedPoint2D_f32__UnknownUnit point_a,
  TypedPoint2D_f32__LayoutUnit point_b,
  Point2D_f32 point_c,
  LayoutPoint2D point_d,
  TypedRect_f32__UnknownUnit rect_a,
  TypedRect_f32__LayoutUnit rect_b,
  Rect_f32 rect_c,
  LayoutRect rect_d,
  TypedTransform2D_f32__UnknownUnit__LayoutUnit transform_a,
  TypedTransform2D_f32__LayoutUnit__UnknownUnit transform_b);

}
