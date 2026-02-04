using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct ExportMe
{

  public required ulong val;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct ExportMe2
{

  public required ulong val;
}
public static partial class Api
{

  public const byte EXPORT_ME_TOO = (byte)42;

  [LibraryImport("library", EntryPoint = "export_me")]
  public unsafe static partial void export_me(ExportMe* val);

  [LibraryImport("library", EntryPoint = "export_me_2")]
  public unsafe static partial void export_me_2(ExportMe2* arg0);

  [LibraryImport("library", EntryPoint = "from_really_nested_mod")]
  public unsafe static partial void from_really_nested_mod();

}
