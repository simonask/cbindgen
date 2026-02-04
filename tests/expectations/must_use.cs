using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct MaybeOwnedPtr_i32
{
  public enum MaybeOwnedPtr_i32_Tag : byte
  {
    Owned_i32,
    None_i32,
  };
  private MaybeOwnedPtr_i32_Tag _tag;
  private EnumData _data;
  [StructLayout(LayoutKind.Explicit)]
  private struct EnumData
  {

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Owned_Body_i32()
    {
      public required int* owned;
    }

  }

}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct OwnedPtr_i32
{

  public required int* ptr;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "maybe_consume")]
  public unsafe static partial MaybeOwnedPtr_i32 maybe_consume(OwnedPtr_i32 input);

}
