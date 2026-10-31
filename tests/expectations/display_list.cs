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
  private Fill fill;
  [FieldOffset(0)]
  private Image image;

  public DisplayItem(Fill fill)
  {
    this.fill = fill;
  }
  public DisplayItem(Image image)
  {
    this.image = image;
  }
  public DisplayItem(ClearScreen _)
  {
    _tag = Tag.ClearScreen;
  }

  public static implicit operator DisplayItem(Fill value) => new(value);
  public static implicit operator DisplayItem(Image value) => new(value);
  public static implicit operator DisplayItem(ClearScreen value) => new(value);

  public readonly bool TryGetValue(out Fill value)
  {
    value = _tag == Tag.Fill ? fill : default;
    return _tag == Tag.Fill;
  }
  public readonly bool TryGetValue(out Image value)
  {
    value = _tag == Tag.Image ? image : default;
    return _tag == Tag.Image;
  }
  public readonly bool TryGetValue(out ClearScreen value)
  {
    value = default;
    return _tag == Tag.ClearScreen;
  }

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
  public record struct Fill()
  {
    private readonly Tag _tag = Tag.Fill;
    public required Rect Item1;
    public required Color Item2;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Fill(Rect item1, Color item2) : this()
    {
      this.Item1 = item1;
      this.Item2 = item2;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Image()
  {
    private readonly Tag _tag = Tag.Image;
    public required uint id;
    public required Rect bounds;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Image(uint id, Rect bounds) : this()
    {
      this.id = id;
      this.bounds = bounds;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  public record struct ClearScreen();
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "push_item")]
  [return: MarshalAs(UnmanagedType.U1)]
  public unsafe static partial bool push_item(DisplayItem item);

}
