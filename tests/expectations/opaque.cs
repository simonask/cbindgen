using System.Runtime.InteropServices;
using FastHashMap_i32__i32 = HashMap_i32__i32__BuildHasherDefault_DefaultHasher;
// FastHashMap_i32__i32
using Foo = HashMap_i32__i32__BuildHasherDefault_DefaultHasher;
using Bar = Result_Foo;

public readonly struct HashMap_i32__i32__BuildHasherDefault_DefaultHasher
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}

public readonly struct Result_Foo
{
  #pragma warning disable CS0169
  private readonly byte _opaque;
  #pragma warning restore CS0169
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo* a, Bar* b);

}
