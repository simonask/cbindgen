using System.Runtime.InteropServices;
public static partial class Api
{

  // cbindgen: Skipped 'FOO', because it is a static extern.

  /// <summary>
  ///      The root of all evil.
  ///     
  ///      But at least it contains some more documentation as someone would expect
  ///      from a simple test case like this.
  ///     
  ///      # Hint
  ///     
  ///      Always ensure that everything is properly documented, even if you feel lazy.
  ///      **Sometimes** it is also helpful to include some markdown formatting.
  ///     
  ///      ////////////////////////////////////////////////////////////////////////////
  ///     
  ///      Attention:
  ///     
  ///         Rust is going to trim all leading `/` symbols. If you want to use them as a
  ///         marker you need to add at least a single whitespace inbetween the tripple
  ///         slash doc-comment marker and the rest.
  ///     
  /// </summary>
  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root();

}
