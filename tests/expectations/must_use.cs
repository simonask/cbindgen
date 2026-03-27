using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct MaybeOwnedPtr_i32
{
  public enum Tag : byte
  {
    Owned_i32,
    None_i32,
  };
  private Tag _tag;
  private MaybeOwnedPtr_i32_Data _data;

  public unsafe static MaybeOwnedPtr_i32 Owned_i32(int* owned)
     => new(new Owned_Body_i32
      {
        owned = owned,

      });
  public static MaybeOwnedPtr_i32 None_i32() => new() { _tag = Tag.None_i32 };

  public MaybeOwnedPtr_i32(Owned_Body_i32 owned)
  {
    _tag = Tag.Owned_i32;
    _data.owned = owned;
  }


  public static implicit operator MaybeOwnedPtr_i32(Owned_Body_i32 value) => new(value);

  public readonly bool IsOwned_i32 => _tag == Tag.Owned_i32;
  public readonly bool IsNone_i32 => _tag == Tag.None_i32;

  public readonly Owned_Body_i32? AsOwned_i32 => _tag == Tag.Owned_i32 ? _data.owned : null;

  public override readonly bool Equals(object? obj) => obj is MaybeOwnedPtr_i32 other && Equals(other);
  public readonly bool Equals(MaybeOwnedPtr_i32 other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.Owned_i32 => _data.owned.Equals(other._data.owned),
      _ => true,

    };

  }
  public static bool operator ==(MaybeOwnedPtr_i32 left, MaybeOwnedPtr_i32 right) => left.Equals(right);
  public static bool operator !=(MaybeOwnedPtr_i32 left, MaybeOwnedPtr_i32 right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.Owned_i32:
      {
        hashCode.Add(_data.owned);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append("MaybeOwnedPtr_i32.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.Owned_i32:
      {
        stringBuilder.Append(" { ");
        _data.owned.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct MaybeOwnedPtr_i32_Data
  {
    [FieldOffset(0)]
    public Owned_Body_i32 owned;
  }
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct Owned_Body_i32()
  {
    public required int* owned;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => stringBuilder.Append("...");
  }


}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct OwnedPtr_i32
{

  public required int* ptr;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "maybe_consume")]
  public unsafe static partial MaybeOwnedPtr_i32 maybe_consume(OwnedPtr_i32 input);

}
