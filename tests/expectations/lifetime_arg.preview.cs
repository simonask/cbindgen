using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial  struct A
{

  public required int* data;
}

[StructLayout(LayoutKind.Sequential)]
[System.Runtime.CompilerServices.Union]
public struct E
{
  public enum Tag
  {
    V,
    U,
  };
  private Tag _tag;
  private E_Data _data;

  public E(V _)
  {
    _tag = Tag.V;
  }
  public E(U u)
  {
    _tag = Tag.U;
    _data.u = u;
  }

  public static implicit operator E(V value) => new(value);
  public static implicit operator E(U value) => new(value);

  public readonly object? Value => _tag switch
  {
    Tag.V => new V(),
    Tag.U => _data.u,
    _ => null,
  };
  public readonly bool HasValue => _tag switch
  {
    Tag.V => true,
    Tag.U => true,
    _ => false,
  };
  public readonly bool TryGetValue(out V value)
  {
    value = default;
    return _tag == Tag.V;
  }
  public readonly bool TryGetValue(out U value)
  {
    value = _tag == Tag.U ? _data.u : default;
    return _tag == Tag.U;
  }

  public override readonly bool Equals(object? obj) => obj is E other && Equals(other);
  public readonly bool Equals(E other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.U => _data.u.Equals(other._data.u),
      _ => true,

    };

  }
  public static bool operator ==(E left, E right) => left.Equals(right);
  public static bool operator !=(E left, E right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.U:
      {
        hashCode.Add(_data.u);
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

      case Tag.U:
      {
        stringBuilder.Append(" { ");
        _data.u.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct E_Data
  {
    [FieldOffset(0)]
    public safe U u;
  }
  public record struct V();
  [StructLayout(LayoutKind.Sequential)]
  public  struct U()
  {
    public required byte* Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public U(byte* value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => stringBuilder.Append("...");
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public safe static partial void root(A _a, E _e);

}
