using System.Runtime.InteropServices;
using FastHashMap_i32__i32 = HashMap_i32__i32__BuildHasherDefault_DefaultHasher;
// FastHashMap_i32__i32
using Foo = HashMap_i32__i32__BuildHasherDefault_DefaultHasher;
using Bar = Result_Foo;

public readonly struct HashMap_i32__i32__BuildHasherDefault_DefaultHasher
{
  private readonly byte _opaque;
}

public readonly struct Result_Foo
{
  private readonly byte _opaque;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo* a, Bar* b);

}
