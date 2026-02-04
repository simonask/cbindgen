using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct A
{

  public required int x;
  public required float y;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct B
{

  public required A data;
}
public static partial class Api
{

}
