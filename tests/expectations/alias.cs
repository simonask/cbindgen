using System.Runtime.InteropServices;
using IntFoo = Foo_i32;
using DoubleFoo = Foo_f64;
using Unit = int;
using SpecialStatus = Status;

public enum Status : uint
{
  Ok,
  Err,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Dep
{

  public required int a;
  public required float b;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo_i32
{

  public required int a;
  public required int b;
  public required Dep c;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo_f64
{

  public required double a;
  public required double b;
  public required Dep c;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(IntFoo x, DoubleFoo y, Unit z, SpecialStatus w);

}
