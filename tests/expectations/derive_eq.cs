using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{

  public required bool a { readonly get => _a != 0; set => _a = value ? (byte)1 : (byte)0; }
  private byte _a;
  public required int b;
}

[StructLayout(LayoutKind.Explicit)]
public struct Bar
{
  public enum Tag : byte
  {
    Baz,
    Bazz,
    FooNamed,
    FooParen,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Bazz_Body bazz;
  [FieldOffset(0)]
  private FooNamed_Body foo_named;
  [FieldOffset(0)]
  private FooParen_Body foo_paren;

  public static Bar Baz() => new() { _tag = Tag.Baz };
  public static Bar Bazz(Foo named)
     => new(new Bazz_Body
      {
        named = named,

      });
  public static Bar FooNamed(int different, uint fields)
     => new(new FooNamed_Body
      {
        different = different,
        fields = fields,

      });
  public static Bar FooParen(int _0, Foo _1)
     => new(new FooParen_Body
      {
        _0 = _0,
        _1 = _1,

      });


  public Bar(Bazz_Body bazz)
  {
    this.bazz = bazz;
  }
  public Bar(FooNamed_Body foo_named)
  {
    this.foo_named = foo_named;
  }
  public Bar(FooParen_Body foo_paren)
  {
    this.foo_paren = foo_paren;
  }

  public static implicit operator Bar(Bazz_Body value) => new(value);
  public static implicit operator Bar(FooNamed_Body value) => new(value);
  public static implicit operator Bar(FooParen_Body value) => new(value);

  public readonly bool IsBaz => _tag == Tag.Baz;
  public readonly bool IsBazz => _tag == Tag.Bazz;
  public readonly bool IsFooNamed => _tag == Tag.FooNamed;
  public readonly bool IsFooParen => _tag == Tag.FooParen;

  public readonly Bazz_Body? AsBazz => _tag == Tag.Bazz ? bazz : null;
  public readonly FooNamed_Body? AsFooNamed => _tag == Tag.FooNamed ? foo_named : null;
  public readonly FooParen_Body? AsFooParen => _tag == Tag.FooParen ? foo_paren : null;

  public override readonly bool Equals(object? obj) => obj is Bar other && Equals(other);
  public readonly bool Equals(Bar other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.Bazz => bazz.Equals(other.bazz),
      Tag.FooNamed => foo_named.Equals(other.foo_named),
      Tag.FooParen => foo_paren.Equals(other.foo_paren),
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
      case Tag.Bazz:
      {
        hashCode.Add(bazz);
        break;
      }
      case Tag.FooNamed:
      {
        hashCode.Add(foo_named);
        break;
      }
      case Tag.FooParen:
      {
        hashCode.Add(foo_paren);
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

      case Tag.Bazz:
      {
        stringBuilder.Append(" { ");
        bazz.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.FooNamed:
      {
        stringBuilder.Append(" { ");
        foo_named.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.FooParen:
      {
        stringBuilder.Append(" { ");
        foo_paren.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }



  [StructLayout(LayoutKind.Sequential)]
  public record struct Bazz_Body()
  {
    private readonly Tag _tag = Tag.Bazz;
    public required Foo named;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct FooNamed_Body()
  {
    private readonly Tag _tag = Tag.FooNamed;
    public required int different;
    public required uint fields;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct FooParen_Body()
  {
    private readonly Tag _tag = Tag.FooParen;
    public required int _0;
    public required Foo _1;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial Foo root(Bar aBar);

}
