using System.Runtime.InteropServices;

public readonly struct Option_i32
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct Result_i32__String
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct Vec_String
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Vec_String* a, Option_i32* b, Result_i32__String* c);

}
