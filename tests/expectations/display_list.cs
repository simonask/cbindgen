using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Rect
{

  public required float x;
  public required float y;
  public required float w;
  public required float h;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Color
{

  public required byte r;
  public required byte g;
  public required byte b;
  public required byte a;
}

[StructLayout(LayoutKind.Explicit)]
public struct DisplayItem
{
  public enum DisplayItem_Tag : byte
  {
    Fill,
    Image,
    ClearScreen,
  };

  [StructLayout(LayoutKind.Sequential)]
  public record struct Fill_Body()
  {
    private readonly DisplayItem_Tag _tag = DisplayItem_Tag.Fill;
    public required Rect _0;
    public required Color _1;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Image_Body()
  {
    private readonly DisplayItem_Tag _tag = DisplayItem_Tag.Image;
    public required uint id;
    public required Rect bounds;
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "push_item")]
  [return: MarshalAs(UnmanagedType.U1)]
  public unsafe static partial byte push_item(DisplayItem item);

}
