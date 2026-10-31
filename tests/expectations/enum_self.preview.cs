using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial  struct Foo_Bar
{

  public required int* something;
}

[StructLayout(LayoutKind.Explicit)]
[System.Runtime.CompilerServices.Union]
public struct Bar
{
  public enum Tag : byte
  {
    Min,
    Max,
    Other,
  };
  [FieldOffset(0)]
  private safe Tag _tag;
  [FieldOffset(0)]
  private safe Min min;
  [FieldOffset(0)]
  private safe Max max;

  public Bar(Min min)
  {
    this.min = min;
  }
  public Bar(Max max)
  {
    this.max = max;
  }
  public Bar(Other _)
  {
    _tag = Tag.Other;
  }

  public static implicit operator Bar(Min value) => new(value);
  public static implicit operator Bar(Max value) => new(value);
  public static implicit operator Bar(Other value) => new(value);

  public readonly object? Value => _tag switch
  {
    Tag.Min => min,
    Tag.Max => max,
    Tag.Other => new Other(),
    _ => null,
  };
  public readonly bool HasValue => _tag switch
  {
    Tag.Min => true,
    Tag.Max => true,
    Tag.Other => true,
    _ => false,
  };
  public readonly bool TryGetValue(out Min value)
  {
    value = _tag == Tag.Min ? min : default;
    return _tag == Tag.Min;
  }
  public readonly bool TryGetValue(out Max value)
  {
    value = _tag == Tag.Max ? max : default;
    return _tag == Tag.Max;
  }
  public readonly bool TryGetValue(out Other value)
  {
    value = default;
    return _tag == Tag.Other;
  }

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
  public record struct Min()
  {
    private readonly Tag _tag = Tag.Min;
    public required Foo_Bar Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Min(Foo_Bar value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Max()
  {
    private readonly Tag _tag = Tag.Max;
    public required Foo_Bar Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Max(Foo_Bar value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  public record struct Other();
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public safe static partial void root(Bar b);

}
