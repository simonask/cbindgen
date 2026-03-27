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
  public enum Tag : byte
  {
    Fill,
    Image,
    ClearScreen,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Fill_Body fill;
  [FieldOffset(0)]
  private Image_Body image;

  public static DisplayItem Fill(Rect _0, Color _1)
     => new(new Fill_Body
      {
        _0 = _0,
        _1 = _1,

      });
  public static DisplayItem Image(uint id, Rect bounds)
     => new(new Image_Body
      {
        id = id,
        bounds = bounds,

      });
  public static DisplayItem ClearScreen() => new() { _tag = Tag.ClearScreen };

  public DisplayItem(Fill_Body fill)
  {
    this.fill = fill;
  }
  public DisplayItem(Image_Body image)
  {
    this.image = image;
  }


  public static implicit operator DisplayItem(Fill_Body value) => new(value);
  public static implicit operator DisplayItem(Image_Body value) => new(value);

  public readonly bool IsFill => _tag == Tag.Fill;
  public readonly bool IsImage => _tag == Tag.Image;
  public readonly bool IsClearScreen => _tag == Tag.ClearScreen;

  public readonly Fill_Body? AsFill => _tag == Tag.Fill ? fill : null;
  public readonly Image_Body? AsImage => _tag == Tag.Image ? image : null;

  public override readonly bool Equals(object? obj) => obj is DisplayItem other && Equals(other);
  public readonly bool Equals(DisplayItem other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.Fill => fill.Equals(other.fill),
      Tag.Image => image.Equals(other.image),
      _ => true,

    };

  }
  public static bool operator ==(DisplayItem left, DisplayItem right) => left.Equals(right);
  public static bool operator !=(DisplayItem left, DisplayItem right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    switch (_tag)
    {
      case Tag.Fill:
      {
        hashCode.Add(fill);
        break;
      }
      case Tag.Image:
      {
        hashCode.Add(image);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append("DisplayItem.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.Fill:
      {
        stringBuilder.Append(" { ");
        fill.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.Image:
      {
        stringBuilder.Append(" { ");
        image.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }


  [StructLayout(LayoutKind.Sequential)]
  public record struct Fill_Body()
  {
    private readonly Tag _tag = Tag.Fill;
    public required Rect _0;
    public required Color _1;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Image_Body()
  {
    private readonly Tag _tag = Tag.Image;
    public required uint id;
    public required Rect bounds;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "push_item")]
  [return: MarshalAs(UnmanagedType.U1)]
  public unsafe static partial byte push_item(DisplayItem item);

}
