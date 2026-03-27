using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct A
{

  public required int* data;
}

[StructLayout(LayoutKind.Sequential)]
public struct E
{
  public enum Tag
  {
    V,
    U,
  };
  private Tag _tag;
  private E_Data _data;

  public static E V() => new() { _tag = Tag.V };
  public unsafe static E U(byte* u)
     => new(new U_Body
      {
        u = u,

      });


  public E(U_Body u)
  {
    _tag = Tag.U;
    _data.u = u;
  }

  public static implicit operator E(U_Body value) => new(value);

  public readonly bool IsV => _tag == Tag.V;
  public readonly bool IsU => _tag == Tag.U;

  public readonly U_Body? AsU => _tag == Tag.U ? _data.u : null;

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
    stringBuilder.Append("E.");
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
    public U_Body u;
  }

  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct U_Body()
  {
    public required byte* u;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => stringBuilder.Append("...");
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A _a, E _e);

}
