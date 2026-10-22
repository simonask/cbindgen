using System.Runtime.InteropServices;
public static partial class Api
{

  /// <summary>
  ///     With doc attr, each attr contribute to one line of document
  ///     like this one with a new line character at its end
  ///     and this one as well. So they are in the same paragraph
  ///     
  ///     We treat empty doc comments as empty lines, so they break to the next paragraph.
  ///     
  ///      Newlines are preserved with leading spaces added
  ///      to prettify and avoid misinterpreting leading symbols.
  ///     like headings and lists.
  ///     
  ///      Line ends with two new lines
  ///     
  ///      Should break to next paragraph
  /// </summary>
  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root();

}
