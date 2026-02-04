using System.Runtime.InteropServices;

public enum BindingType : uint
{
  Buffer = 0,
  NotBuffer = 1,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct BindGroupLayoutEntry
{

  public required BindingType ty;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(BindGroupLayoutEntry entry);

}
