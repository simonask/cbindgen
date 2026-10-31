using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
[System.Runtime.CompilerServices.Union]
public struct Foo
{
  public enum Tag
  {
    A,
  };
  private Tag _tag;
  private Foo_Data _data;

  public Foo(A a)
  {
    _tag = Tag.A;
    _data.a = a;
  }

  public static implicit operator Foo(A value) => new(value);

  public readonly object? Value => _tag switch
  {
    Tag.A => _data.a,
    _ => null,
  };
  public readonly bool HasValue => _tag switch
  {
    Tag.A => true,
    _ => false,
  };
  public readonly bool TryGetValue(out A value)
  {
    value = _tag == Tag.A ? _data.a : default;
    return _tag == Tag.A;
  }

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
    public safe A a;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct A()
  {
    public required InlineArray20_float Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public A(InlineArray20_float value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public safe static partial void root(Foo a);

}
[System.Runtime.CompilerServices.InlineArray(20)]
public struct InlineArray20_float
{
  private float _data;
}
