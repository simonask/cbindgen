using System.Runtime.InteropServices;
using MyCallback = nint;
using MyOtherCallback = nint;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "my_function")]
  public unsafe static partial void my_function(MyCallback a, MyOtherCallback b);

}
