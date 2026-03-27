using System.Runtime.InteropServices;

#if (PLATFORM_UNIX && X11)
public enum FooType : uint
{
  A,
  B,
  C,
};
#endif

#if (PLATFORM_WIN || M_32)
public enum BarType : uint
{
  A,
  B,
  C,
};
#endif

[Flags]
public enum Flags : byte
{
  /// <summary>
  ///      none
  /// </summary>
  NONE = unchecked((byte)0),
#if PLATFORM_WIN
  A = unchecked((byte)(1 << (int)0)),
#endif
#if PLATFORM_UNIX
  A = unchecked((byte)(1 << (int)1)),
#endif
#if PLATFORM_WIN
  B = unchecked((byte)(Flags.A | (1 << (int)3))),
#endif
#if PLATFORM_UNIX
  B = unchecked((byte)(Flags.A | (1 << (int)4))),
#endif
}


#if (PLATFORM_UNIX && X11)
[StructLayout(LayoutKind.Sequential)]
public  partial record struct FooHandle
{

  public required FooType ty;
  public required Flags flags;
  public required int x;
  public required float y;
}
#endif

[StructLayout(LayoutKind.Explicit)]
public struct C
{
  public enum Tag : byte
  {
    C1,
    C2,
#if PLATFORM_WIN
    C3,
#endif
#if PLATFORM_UNIX
    C5,
#endif
  };
  [FieldOffset(0)]
  private Tag _tag;
#if PLATFORM_UNIX
  [FieldOffset(0)]
  private C5_Body c5;

#endif
  public static C C1() => new() { _tag = Tag.C1 };
  public static C C2() => new() { _tag = Tag.C2 };
#if PLATFORM_WIN
  public static C C3() => new() { _tag = Tag.C3 };
#endif
#if PLATFORM_UNIX
  public static C C5(int @int)
     => new(new C5_Body
      {
        @int = @int,

      });
#endif



#if PLATFORM_WIN

#endif
#if PLATFORM_UNIX
  public C(C5_Body c5)
  {
    this.c5 = c5;
  }
#endif

#if PLATFORM_UNIX
  public static implicit operator C(C5_Body value) => new(value);
#endif

  public readonly bool IsC1 => _tag == Tag.C1;
  public readonly bool IsC2 => _tag == Tag.C2;
#if PLATFORM_WIN
  public readonly bool IsC3 => _tag == Tag.C3;
#endif
#if PLATFORM_UNIX
  public readonly bool IsC5 => _tag == Tag.C5;
#endif

#if PLATFORM_UNIX
  public readonly C5_Body? AsC5 => _tag == Tag.C5 ? c5 : null;
#endif

  public override readonly bool Equals(object? obj) => obj is C other && Equals(other);
  public readonly bool Equals(C other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
#if PLATFORM_UNIX
      Tag.C5 => c5.Equals(other.c5),
#endif
      _ => true,

    };

  }
  public static bool operator ==(C left, C right) => left.Equals(right);
  public static bool operator !=(C left, C right) => !left.Equals(right);

  public override readonly int GetHashCode()
  {
    var hashCode = new HashCode();
    switch (_tag)
    {
#if PLATFORM_UNIX
      case Tag.C5:
      {
        hashCode.Add(c5);
        break;
      }
#endif
      default: break;
    }
    return hashCode.ToHashCode();
  }

  public override readonly string ToString()
  {
    var stringBuilder = new System.Text.StringBuilder();
    stringBuilder.Append("C.");
    stringBuilder.Append(_tag.ToString());
    switch (_tag)
    {

#if PLATFORM_UNIX
      case Tag.C5:
      {
        stringBuilder.Append(" { ");
        c5.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
#endif
      default: break;
    }
    return stringBuilder.ToString();
  }




#if PLATFORM_WIN

#endif
#if PLATFORM_UNIX
#if PLATFORM_UNIX
  [StructLayout(LayoutKind.Sequential)]
  public record struct C5_Body()
  {
    private readonly Tag _tag = Tag.C5;
    public required int @int;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
#endif
#endif
}

#if (PLATFORM_WIN || M_32)
[StructLayout(LayoutKind.Sequential)]
public  partial record struct BarHandle
{

  public required BarType ty;
  public required int x;
  public required float y;
}
#endif

[StructLayout(LayoutKind.Sequential)]
public  partial record struct ConditionalField
{

  public static readonly ConditionalField ZERO = new ConditionalField
  {
#if X11
    @field = (int)0,

#endif
  };

  public static readonly ConditionalField ONE = new ConditionalField
  {
#if X11
    @field = (int)1,

#endif
  };

#if X11
  public required int @field;
#endif
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Normal
{

  public required int x;
  public required float y;
}
public static partial class Api
{

  // cbindgen: Skipped 'global_array_with_different_sizes', because it is a static extern.

  // cbindgen: Skipped 'global_array_with_different_sizes', because it is a static extern.

#if (PLATFORM_UNIX && X11)
  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(FooHandle a, C c);
#endif

#if (PLATFORM_WIN || M_32)
  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(BarHandle a, C c);
#endif

  [LibraryImport("library", EntryPoint = "cond")]
  public unsafe static partial void cond(ConditionalField a);

#if PLATFORM_WIN
  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial int foo();
#endif

#if PLATFORM_WIN
  [LibraryImport("library", EntryPoint = "bar")]
  public unsafe static partial void bar(Normal a);
#endif

}
