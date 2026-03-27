using System.Runtime.InteropServices;

public enum C : uint
{
  X = 2,
  Y,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct A
{

  public required int _0;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct B
{

  public required int x;
  public required float y;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct D
{

  public required byte List;
  public required nuint Of;
  public required B Things;
}

[StructLayout(LayoutKind.Explicit)]
public struct F
{
  public enum Tag : byte
  {
    Foo,
    Bar,
    Baz,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Foo_Body foo;
  [FieldOffset(0)]
  private Bar_Body bar;

  public static F Foo(short foo)
     => new(new Foo_Body
      {
        foo = foo,

      });
  public static F Bar(byte x, short y)
     => new(new Bar_Body
      {
        x = x,
        y = y,

      });
  public static F Baz() => new() { _tag = Tag.Baz };

  public F(Foo_Body foo)
  {
    this.foo = foo;
  }
  public F(Bar_Body bar)
  {
    this.bar = bar;
  }


  public static implicit operator F(Foo_Body value) => new(value);
  public static implicit operator F(Bar_Body value) => new(value);

  public readonly bool IsFoo => _tag == Tag.Foo;
  public readonly bool IsBar => _tag == Tag.Bar;
  public readonly bool IsBaz => _tag == Tag.Baz;

  public readonly Foo_Body? AsFoo => _tag == Tag.Foo ? foo : null;
  public readonly Bar_Body? AsBar => _tag == Tag.Bar ? bar : null;

  public override readonly bool Equals(object? obj) => obj is F other && Equals(other);
  public readonly bool Equals(F other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.Foo => foo.Equals(other.foo),
      Tag.Bar => bar.Equals(other.bar),
      _ => true,

    };

  }
  public static bool operator ==(F left, F right) => left.Equals(right);
  public static bool operator !=(F left, F right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    switch (_tag)
    {
      case Tag.Foo:
      {
        hashCode.Add(foo);
        break;
      }
      case Tag.Bar:
      {
        hashCode.Add(bar);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append("F.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.Foo:
      {
        stringBuilder.Append(" { ");
        foo.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.Bar:
      {
        stringBuilder.Append(" { ");
        bar.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }


  [StructLayout(LayoutKind.Sequential)]
  public record struct Foo_Body()
  {
    private readonly Tag _tag = Tag.Foo;
    public required short foo;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar_Body()
  {
    private readonly Tag _tag = Tag.Bar;
    public required byte x;
    public required short y;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct H
{
  public enum Tag : byte
  {
    Hello,
    There,
    Everyone,
  };
  private Tag _tag;
  private H_Data _data;

  public static H Hello(short hello)
     => new(new Hello_Body
      {
        hello = hello,

      });
  public static H There(byte x, short y)
     => new(new There_Body
      {
        x = x,
        y = y,

      });
  public static H Everyone() => new() { _tag = Tag.Everyone };

  public H(Hello_Body hello)
  {
    _tag = Tag.Hello;
    _data.hello = hello;
  }
  public H(There_Body there)
  {
    _tag = Tag.There;
    _data.there = there;
  }


  public static implicit operator H(Hello_Body value) => new(value);
  public static implicit operator H(There_Body value) => new(value);

  public readonly bool IsHello => _tag == Tag.Hello;
  public readonly bool IsThere => _tag == Tag.There;
  public readonly bool IsEveryone => _tag == Tag.Everyone;

  public readonly Hello_Body? AsHello => _tag == Tag.Hello ? _data.hello : null;
  public readonly There_Body? AsThere => _tag == Tag.There ? _data.there : null;

  public override readonly bool Equals(object? obj) => obj is H other && Equals(other);
  public readonly bool Equals(H other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.Hello => _data.hello.Equals(other._data.hello),
      Tag.There => _data.there.Equals(other._data.there),
      _ => true,

    };

  }
  public static bool operator ==(H left, H right) => left.Equals(right);
  public static bool operator !=(H left, H right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.Hello:
      {
        hashCode.Add(_data.hello);
        break;
      }
      case Tag.There:
      {
        hashCode.Add(_data.there);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append("H.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.Hello:
      {
        stringBuilder.Append(" { ");
        _data.hello.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.There:
      {
        stringBuilder.Append(" { ");
        _data.there.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct H_Data
  {
    [FieldOffset(0)]
    public Hello_Body hello;
    [FieldOffset(0)]
    public There_Body there;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Hello_Body()
  {
    public required short hello;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct There_Body()
  {
    public required byte x;
    public required short y;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }


}

[StructLayout(LayoutKind.Sequential)]
public struct I
{
  public enum Tag : byte
  {
    ThereAgain,
    SomethingElse,
  };
  private Tag _tag;
  private I_Data _data;

  public static I ThereAgain(byte x, short y)
     => new(new ThereAgain_Body
      {
        x = x,
        y = y,

      });
  public static I SomethingElse() => new() { _tag = Tag.SomethingElse };

  public I(ThereAgain_Body there_again)
  {
    _tag = Tag.ThereAgain;
    _data.there_again = there_again;
  }


  public static implicit operator I(ThereAgain_Body value) => new(value);

  public readonly bool IsThereAgain => _tag == Tag.ThereAgain;
  public readonly bool IsSomethingElse => _tag == Tag.SomethingElse;

  public readonly ThereAgain_Body? AsThereAgain => _tag == Tag.ThereAgain ? _data.there_again : null;

  public override readonly bool Equals(object? obj) => obj is I other && Equals(other);
  public readonly bool Equals(I other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.ThereAgain => _data.there_again.Equals(other._data.there_again),
      _ => true,

    };

  }
  public static bool operator ==(I left, I right) => left.Equals(right);
  public static bool operator !=(I left, I right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.ThereAgain:
      {
        hashCode.Add(_data.there_again);
        break;
      }
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append("I.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

      case Tag.ThereAgain:
      {
        stringBuilder.Append(" { ");
        _data.there_again.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct I_Data
  {
    [FieldOffset(0)]
    public ThereAgain_Body there_again;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct ThereAgain_Body()
  {
    public required byte x;
    public required short y;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }


}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A a, B b, C c, D d, F f, H h, I i);

}
