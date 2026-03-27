using System.Runtime.InteropServices;

public enum A : byte
{
  A_A1,
  A_A2,
  A_A3,
  /// <summary>
  ///      Must be last for serialization purposes
  /// </summary>
  A_Sentinel,
};

public enum B : byte
{
  B_B1,
  B_B2,
  B_B3,
  /// <summary>
  ///      Must be last for serialization purposes
  /// </summary>
  B_Sentinel,
};

[StructLayout(LayoutKind.Explicit)]
public struct C
{
  public enum Tag : byte
  {
    C_C1,
    C_C2,
    C_C3,
    /// <summary>
    ///      Must be last for serialization purposes
    /// </summary>
    C_Sentinel,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private C_C1_Body c1;
  [FieldOffset(0)]
  private C_C2_Body c2;

  public static C C1(uint a)
     => new(new C_C1_Body
      {
        a = a,

      });
  public static C C2(uint b)
     => new(new C_C2_Body
      {
        b = b,

      });
  public static C C3() => new() { _tag = Tag.C_C3 };
  /// <summary>
  ///      Must be last for serialization purposes
  /// </summary>
  public static C Sentinel() => new() { _tag = Tag.C_Sentinel };

  public C(C_C1_Body c1)
  {
    this.c1 = c1;
  }
  public C(C_C2_Body c2)
  {
    this.c2 = c2;
  }



  public static implicit operator C(C_C1_Body value) => new(value);
  public static implicit operator C(C_C2_Body value) => new(value);

  public readonly bool IsC1 => _tag == Tag.C_C1;
  public readonly bool IsC2 => _tag == Tag.C_C2;
  public readonly bool IsC3 => _tag == Tag.C_C3;
  public readonly bool IsSentinel => _tag == Tag.C_Sentinel;

  public readonly C_C1_Body? AsC1 => _tag == Tag.C_C1 ? c1 : null;
  public readonly C_C2_Body? AsC2 => _tag == Tag.C_C2 ? c2 : null;

  public override readonly bool Equals(object? obj) => obj is C other && Equals(other);
  public readonly bool Equals(C other)
  {
    if (_tag != other._tag) return false;
    return _tag switch
    {
      Tag.C_C1 => c1.Equals(other.c1),
      Tag.C_C2 => c2.Equals(other.c2),
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
      case Tag.C_C1:
      {
        hashCode.Add(c1);
        break;
      }
      case Tag.C_C2:
      {
        hashCode.Add(c2);
        break;
      }
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

      case Tag.C_C1:
      {
        stringBuilder.Append(" { ");
        c1.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      case Tag.C_C2:
      {
        stringBuilder.Append(" { ");
        c2.PrintMembersInternal(stringBuilder);
        stringBuilder.Append(" }");
        break;
      }
      default: break;
    }
    return stringBuilder.ToString();
  }


  [StructLayout(LayoutKind.Sequential)]
  public record struct C_C1_Body()
  {
    private readonly Tag _tag = Tag.C_C1;
    public required uint a;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct C_C2_Body()
  {
    private readonly Tag _tag = Tag.C_C2;
    public required uint b;
    internal readonly void PrintMembersInternal(System.Text.StringBuilder stringBuilder) => PrintMembers(stringBuilder);
  }


}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(A a, B b, C c);

}
