using System.Runtime.InteropServices;

public enum MyCLikeEnum
{
  Foo1,
  Bar1,
  Baz1,
};

public enum MyCLikeEnum_Prepended
{
  Foo1_Prepended,
  Bar1_Prepended,
  Baz1_Prepended,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct MyFancyStruct
{

  public required int i;
}

[StructLayout(LayoutKind.Sequential)]
[System.Runtime.CompilerServices.Union]
public struct MyFancyEnum
{
  public enum Tag
  {
    Foo,
    Bar,
    Baz,
  };
  private Tag _tag;
  private MyFancyEnum_Data _data;

  public MyFancyEnum(Foo _)
  {
    _tag = Tag.Foo;
  }
  public MyFancyEnum(Bar bar)
  {
    _tag = Tag.Bar;
    _data.bar = bar;
  }
  public MyFancyEnum(Baz baz)
  {
    _tag = Tag.Baz;
    _data.baz = baz;
  }

  public static implicit operator MyFancyEnum(Foo value) => new(value);
  public static implicit operator MyFancyEnum(Bar value) => new(value);
  public static implicit operator MyFancyEnum(Baz value) => new(value);

  public readonly object? Value => _tag switch
  {
    Tag.Foo => new Foo(),
    Tag.Bar => _data.bar,
    Tag.Baz => _data.baz,
    _ => null,
  };
  public readonly bool HasValue => _tag switch
  {
    Tag.Foo => true,
    Tag.Bar => true,
    Tag.Baz => true,
    _ => false,
  };
  public readonly bool TryGetValue(out Foo value)
  {
    value = default;
    return _tag == Tag.Foo;
  }
  public readonly bool TryGetValue(out Bar value)
  {
    value = _tag == Tag.Bar ? _data.bar : default;
    return _tag == Tag.Bar;
  }
  public readonly bool TryGetValue(out Baz value)
  {
    value = _tag == Tag.Baz ? _data.baz : default;
    return _tag == Tag.Baz;
  }

  public override readonly bool Equals(object? obj) => obj is MyFancyEnum other && Equals(other);
  public readonly bool Equals(MyFancyEnum other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.Bar => _data.bar.Equals(other._data.bar),
      Tag.Baz => _data.baz.Equals(other._data.baz),
      _ => true,

    };

  }
  public static bool operator ==(MyFancyEnum left, MyFancyEnum right) => left.Equals(right);
  public static bool operator !=(MyFancyEnum left, MyFancyEnum right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.Bar:
      {
        hashCode.Add(_data.bar);
        break;
      }
      case Tag.Baz:
      {
        hashCode.Add(_data.baz);
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

      case Tag.Bar:
      {
        stringBuilder.Append(" { ");
        _data.bar.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.Baz:
      {
        stringBuilder.Append(" { ");
        _data.baz.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct MyFancyEnum_Data
  {
    [FieldOffset(0)]
    public safe Bar bar;
    [FieldOffset(0)]
    public safe Baz baz;
  }
  public record struct Foo();
  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar()
  {
    public required int Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Bar(int value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Baz()
  {
    public required int Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Baz(int value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }

}

[StructLayout(LayoutKind.Explicit)]
public  partial struct MyUnion
{
  [FieldOffset(0)]
  public unsafe float f;
  [FieldOffset(0)]
  public unsafe uint u;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct MyFancyStruct_Prepended
{

  public required int i;
}

[StructLayout(LayoutKind.Sequential)]
[System.Runtime.CompilerServices.Union]
public struct MyFancyEnum_Prepended
{
  public enum Tag
  {
    Foo_Prepended,
    Bar_Prepended,
    Baz_Prepended,
  };
  private Tag _tag;
  private MyFancyEnum_Prepended_Data _data;

  public MyFancyEnum_Prepended(Foo_Prepended _)
  {
    _tag = Tag.Foo_Prepended;
  }
  public MyFancyEnum_Prepended(Bar_Prepended bar_prepended)
  {
    _tag = Tag.Bar_Prepended;
    _data.bar_prepended = bar_prepended;
  }
  public MyFancyEnum_Prepended(Baz_Prepended baz_prepended)
  {
    _tag = Tag.Baz_Prepended;
    _data.baz_prepended = baz_prepended;
  }

  public static implicit operator MyFancyEnum_Prepended(Foo_Prepended value) => new(value);
  public static implicit operator MyFancyEnum_Prepended(Bar_Prepended value) => new(value);
  public static implicit operator MyFancyEnum_Prepended(Baz_Prepended value) => new(value);

  public readonly object? Value => _tag switch
  {
    Tag.Foo_Prepended => new Foo_Prepended(),
    Tag.Bar_Prepended => _data.bar_prepended,
    Tag.Baz_Prepended => _data.baz_prepended,
    _ => null,
  };
  public readonly bool HasValue => _tag switch
  {
    Tag.Foo_Prepended => true,
    Tag.Bar_Prepended => true,
    Tag.Baz_Prepended => true,
    _ => false,
  };
  public readonly bool TryGetValue(out Foo_Prepended value)
  {
    value = default;
    return _tag == Tag.Foo_Prepended;
  }
  public readonly bool TryGetValue(out Bar_Prepended value)
  {
    value = _tag == Tag.Bar_Prepended ? _data.bar_prepended : default;
    return _tag == Tag.Bar_Prepended;
  }
  public readonly bool TryGetValue(out Baz_Prepended value)
  {
    value = _tag == Tag.Baz_Prepended ? _data.baz_prepended : default;
    return _tag == Tag.Baz_Prepended;
  }

  public override readonly bool Equals(object? obj) => obj is MyFancyEnum_Prepended other && Equals(other);
  public readonly bool Equals(MyFancyEnum_Prepended other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.Bar_Prepended => _data.bar_prepended.Equals(other._data.bar_prepended),
      Tag.Baz_Prepended => _data.baz_prepended.Equals(other._data.baz_prepended),
      _ => true,

    };

  }
  public static bool operator ==(MyFancyEnum_Prepended left, MyFancyEnum_Prepended right) => left.Equals(right);
  public static bool operator !=(MyFancyEnum_Prepended left, MyFancyEnum_Prepended right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(_tag);
    switch (_tag)
    {
      case Tag.Bar_Prepended:
      {
        hashCode.Add(_data.bar_prepended);
        break;
      }
      case Tag.Baz_Prepended:
      {
        hashCode.Add(_data.baz_prepended);
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

      case Tag.Bar_Prepended:
      {
        stringBuilder.Append(" { ");
        _data.bar_prepended.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.Baz_Prepended:
      {
        stringBuilder.Append(" { ");
        _data.baz_prepended.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct MyFancyEnum_Prepended_Data
  {
    [FieldOffset(0)]
    public safe Bar_Prepended bar_prepended;
    [FieldOffset(0)]
    public safe Baz_Prepended baz_prepended;
  }
  public record struct Foo_Prepended();
  [StructLayout(LayoutKind.Sequential)]
  public record struct Bar_Prepended()
  {
    public required int Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Bar_Prepended(int value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Baz_Prepended()
  {
    public required int Value;

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Baz_Prepended(int value) : this()
    {
      this.Value = value;
    }
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }

}

[StructLayout(LayoutKind.Explicit)]
public  partial struct MyUnion_Prepended
{
  [FieldOffset(0)]
  public unsafe float f;
  [FieldOffset(0)]
  public unsafe uint u;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public safe static partial void
  root(MyFancyStruct s,
    MyFancyEnum e,
    MyCLikeEnum c,
    MyUnion u,
    MyFancyStruct_Prepended sp,
    MyFancyEnum_Prepended ep,
    MyCLikeEnum_Prepended cp,
    MyUnion_Prepended up);

}
