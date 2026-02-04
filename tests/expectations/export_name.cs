using System.Runtime.InteropServices;
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "do_the_thing_with_export_name")]
  public unsafe static partial void do_the_thing_with_export_name();

  [LibraryImport("library", EntryPoint = "do_the_thing_with_unsafe_export_name")]
  public unsafe static partial void do_the_thing_with_unsafe_export_name();

}
