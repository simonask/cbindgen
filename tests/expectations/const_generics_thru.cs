using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Inner_1
{

  public required byte bytes;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Outer_1
{

  public required Inner_1 inner;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Inner_2
{

  public required System.Runtime.CompilerServices.InlineArray2<byte> bytes;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Outer_2
{

  public required Inner_2 inner;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "one")]
  public unsafe static partial Outer_1 one();

  [LibraryImport("library", EntryPoint = "two")]
  public unsafe static partial Outer_2 two();

}
