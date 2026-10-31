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
  private Bazz bazz;
  [FieldOffset(0)]
  private FooNamed foo_named;
  [FieldOffset(0)]
  private FooParen foo_paren;

  public Bar(Baz _)
  {
    _tag = Tag.Baz;
  }
  public Bar(Bazz bazz)
  {
    this.bazz = bazz;
  }
  public Bar(FooNamed foo_named)
  {
    this.foo_named = foo_named;
  }
  public Bar(FooParen foo_paren)
  {
    this.foo_paren = foo_paren;
  }

  public static implicit operator Bar(Baz value) => new(value);
  public static implicit operator Bar(Bazz value) => new(value);
  public static implicit operator Bar(FooNamed value) => new(value);
  public static implicit operator Bar(FooParen value) => new(value);

  public readonly bool TryGetValue(out Baz value)
  {
    value = default;
    return _tag == Tag.Baz;
  }
  public readonly bool TryGetValue(out Bazz value)
  {
    value = _tag == Tag.Bazz ? bazz : default;
    return _tag == Tag.Bazz;
  }
  public readonly bool TryGetValue(out FooNamed value)
  {
    value = _tag == Tag.FooNamed ? foo_named : default;
    return _tag == Tag.FooNamed;
  }
  public readonly bool TryGetValue(out FooParen value)
  {
    value = _tag == Tag.FooParen ? foo_paren : default;
    return _tag == Tag.FooParen;
  }

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


  public record struct Baz();
  [StructLayout(LayoutKind.Sequential)]
  public record struct Bazz()
  {
    private readonly Tag _tag = Tag.Bazz;
    public required Foo named;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Bazz(Foo named) : this()
    {
      this.named = named;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct FooNamed()
  {
    private readonly Tag _tag = Tag.FooNamed;
    public required int different;
    public required uint fields;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public FooNamed(int different, uint fields) : this()
    {
      this.different = different;
      this.fields = fields;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct FooParen()
  {
    private readonly Tag _tag = Tag.FooParen;
    public required int Item1;
    public required Foo Item2;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public FooParen(int item1, Foo item2) : this()
    {
      this.Item1 = item1;
      this.Item2 = item2;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial Foo root(Bar aBar);

}
