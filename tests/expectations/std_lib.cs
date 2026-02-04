using System.Runtime.InteropServices;

public readonly struct Option_i32
{
  private readonly byte _opaque;
}

public readonly struct Result_i32__String
{
  private readonly byte _opaque;
}

public readonly struct Vec_String
{
  private readonly byte _opaque;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Vec_String* a, Option_i32* b, Result_i32__String* c);

}
