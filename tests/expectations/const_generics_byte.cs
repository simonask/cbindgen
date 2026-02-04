using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Parser_40__41
{

  public required byte* buf;
  public required nuint len;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct Parser_123__125
{

  public required byte* buf;
  public required nuint len;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "init_parens_parser")]
  public unsafe static partial void init_parens_parser(Parser_40__41* p, byte* buf, nuint len);

  [LibraryImport("library", EntryPoint = "destroy_parens_parser")]
  public unsafe static partial void destroy_parens_parser(Parser_40__41* p);

  [LibraryImport("library", EntryPoint = "init_braces_parser")]
  public unsafe static partial void init_braces_parser(Parser_123__125* p, byte* buf, nuint len);

}
