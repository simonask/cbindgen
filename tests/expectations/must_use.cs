using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct MaybeOwnedPtr_i32
{
  public enum Tag : byte
  {
    Owned_i32,
    None_i32,
  };
  private Tag _tag;
  private MaybeOwnedPtr_i32_Data _data;

  public unsafe static MaybeOwnedPtr_i32 Owned_i32(int* owned)
     => new(new Owned_Body_i32
      {
        owned = owned,

      });
  public static MaybeOwnedPtr_i32 None_i32() => new() { _tag = Tag.None_i32 };

  public MaybeOwnedPtr_i32(Owned_Body_i32 owned)
  {
    _tag = Tag.Owned_i32;
    _data.owned = owned;
  }


  public static implicit operator MaybeOwnedPtr_i32(Owned_Body_i32 value) => new(value);

  public readonly bool IsOwned_i32 => _tag == Tag.Owned_i32;
  public readonly bool IsNone_i32 => _tag == Tag.None_i32;

  public readonly Owned_Body_i32? AsOwned_i32 => _tag == Tag.Owned_i32 ? _data.owned : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct MaybeOwnedPtr_i32_Data
  {
    [FieldOffset(0)]
    public Owned_Body_i32 owned;
  }
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct Owned_Body_i32()
  {
    public required int* owned;
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
