using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Foo_Bar
{

  public required int* something;
}

[StructLayout(LayoutKind.Explicit)]
public struct Bar
{
  public enum Tag : byte
  {
    Min,
    Max,
    Other,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Min_Body min;
  [FieldOffset(0)]
  private Max_Body max;

  public static Bar Min(Foo_Bar min)
     => new(new Min_Body
      {
        min = min,

      });
  public static Bar Max(Foo_Bar max)
     => new(new Max_Body
      {
        max = max,

      });
  public static Bar Other() => new() { _tag = Tag.Other };

  public Bar(Min_Body min)
  {
    this.min = min;
  }
  public Bar(Max_Body max)
  {
    this.max = max;
  }


  public static implicit operator Bar(Min_Body value) => new(value);
  public static implicit operator Bar(Max_Body value) => new(value);

  public readonly bool IsMin => _tag == Tag.Min;
  public readonly bool IsMax => _tag == Tag.Max;
  public readonly bool IsOther => _tag == Tag.Other;

  public readonly Min_Body? AsMin => _tag == Tag.Min ? min : null;
  public readonly Max_Body? AsMax => _tag == Tag.Max ? max : null;

  public override readonly bool Equals(object? obj) => obj is Bar other && Equals(other);
  public readonly bool Equals(Bar other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.Min => min.Equals(other.min),
      Tag.Max => max.Equals(other.max),
      _ => true,

    };

  }
  public static bool operator ==(Bar left, Bar right) => left.Equals(right);
  public static bool operator !=(Bar left, Bar right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    switch (_tag)
    {
      case Tag.Min:
      {
        hashCode.Add(min);
        break;
      }
      case Tag.Max:
      {
        hashCode.Add(max);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append("Bar.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.Min:
      {
        stringBuilder.Append(" { ");
        min.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.Max:
      {
        stringBuilder.Append(" { ");
        max.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }


  [StructLayout(LayoutKind.Sequential)]
  public record struct Min_Body()
  {
    private readonly Tag _tag = Tag.Min;
    public required Foo_Bar min;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Max_Body()
  {
    private readonly Tag _tag = Tag.Max;
    public required Foo_Bar max;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Bar b);

}
