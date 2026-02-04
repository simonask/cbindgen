using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct DummyStruct
{

  public required int dummy_field;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "new_dummy")]
  public unsafe static partial DummyStruct new_dummy();

  [LibraryImport("library", EntryPoint = "new_dummy_param")]
  public unsafe static partial DummyStruct new_dummy_param(int dummy_field);

}
