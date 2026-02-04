using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{

  public const long Foo_CONSTANT_I64_BODY = (long)216;
  public required int x;
}
public static partial class Api
{

  public const long CONSTANT_I64 = (long)216;

  public const float CONSTANT_FLOAT32 = (float)312.292;

  public const uint DELIMITER = (uint)':';

  public const uint LEFTCURLY = (uint)'{';
  public static readonly Foo SomeFoo = new Foo
  {
    x = (int)99,

  };

}
