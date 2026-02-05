using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Foo_Bar
{

  public required int* something;
}

[StructLayout(LayoutKind.Explicit)]
public struct Bar
{
  public enum Tag : byte
  {
    Min,
    Max,
    Other,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private Min_Body min;
  [FieldOffset(0)]
  private Max_Body max;

  public static Bar Min(Foo_Bar min)
     => new(new Min_Body
      {
        min = min,

      });
  public static Bar Max(Foo_Bar max)
     => new(new Max_Body
      {
        max = max,

      });
  public static Bar Other() => new() { _tag = Tag.Other };

  public Bar(Min_Body min)
  {
    this.min = min;
  }
  public Bar(Max_Body max)
  {
    this.max = max;
  }


  public static implicit operator Bar(Min_Body value) => new(value);
  public static implicit operator Bar(Max_Body value) => new(value);

  public readonly bool IsMin => _tag == Tag.Min;
  public readonly bool IsMax => _tag == Tag.Max;
  public readonly bool IsOther => _tag == Tag.Other;

  public readonly Min_Body? AsMin => _tag == Tag.Min ? min : null;
  public readonly Max_Body? AsMax => _tag == Tag.Max ? max : null;


  [StructLayout(LayoutKind.Sequential)]
  public record struct Min_Body()
  {
    private readonly Tag _tag = Tag.Min;
    public required Foo_Bar min;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct Max_Body()
  {
    private readonly Tag _tag = Tag.Max;
    public required Foo_Bar max;
  }

}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Bar b);

}
