using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct Foo
{
  public enum Tag
  {
    A,
  };
  private Tag _tag;
  private Foo_Data _data;

  public static Foo A(InlineArray20_float a)
     => new(new A_Body
      {
        a = a,

      });

  public Foo(A_Body a)
  {
    _tag = Tag.A;
    _data.a = a;
  }

  public static implicit operator Foo(A_Body value) => new(value);

  public readonly bool IsA => _tag == Tag.A;

  public readonly A_Body? AsA => _tag == Tag.A ? _data.a : null;

  public override readonly bool Equals(object? obj) => obj is Foo other && Equals(other);
  public readonly bool Equals(Foo other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.A => _data.a.Equals(other._data.a),
      _ => true,

    };

  }
  public static bool operator ==(Foo left, Foo right) => left.Equals(right);
  public static bool operator !=(Foo left, Foo right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.A:
      {
        hashCode.Add(_data.a);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append("Foo.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.A:
      {
        stringBuilder.Append(" { ");
        _data.a.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct Foo_Data
  {
    [FieldOffset(0)]
    public A_Body a;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct A_Body()
  {
    public required InlineArray20_float a;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo a);

}
[System.Runtime.CompilerServices.InlineArray(20)]
public struct InlineArray20_float
{
  private float _data;
}
