using System.Runtime.InteropServices;

#if (PLATFORM_UNIX && X11)
public enum FooType : uint
{
  A,
  B,
  C,
};
#endif

#if (PLATFORM_WIN || M_32)
public enum BarType : uint
{
  A,
  B,
  C,
};
#endif

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Flags
{

  /// <summary>
  /// ///  none
  /// </summary>
  public static readonly Flags NONE = new Flags
  {
    _0 = (byte)(byte)0,

  };

#if PLATFORM_WIN
  public static readonly Flags A = new Flags
  {
    _0 = (byte)(byte)(1 << (int)0),

  };
#endif

#if PLATFORM_UNIX
  public static readonly Flags A = new Flags
  {
    _0 = (byte)(byte)(1 << (int)1),

  };
#endif

#if PLATFORM_WIN
  public static readonly Flags B = new Flags
  {
    _0 = (byte)(byte)((Flags.A)._0 | (1 << (int)3)),

  };
#endif

#if PLATFORM_UNIX
  public static readonly Flags B = new Flags
  {
    _0 = (byte)(byte)((Flags.A)._0 | (1 << (int)4)),

  };
#endif

  public required byte _0;
}

#if (PLATFORM_UNIX && X11)
[StructLayout(LayoutKind.Sequential)]
public  partial record struct FooHandle
{

  public required FooType ty;
  public required Flags flags;
  public required int x;
  public required float y;
}
#endif

[StructLayout(LayoutKind.Explicit)]
public struct C
{
  public enum C_Tag : byte
  {
    C1,
    C2,
#if PLATFORM_WIN
    C3,
#endif
#if PLATFORM_UNIX
    C5,
#endif
  };




#if PLATFORM_UNIX
  [StructLayout(LayoutKind.Sequential)]
  public record struct C5_Body()
  {
    private readonly C_Tag _tag = C_Tag.C5;
    public required int int_;
  }
#endif
}

#if (PLATFORM_WIN || M_32)
[StructLayout(LayoutKind.Sequential)]
public  partial record struct BarHandle
{

  public required BarType ty;
  public required int x;
  public required float y;
}
#endif

[StructLayout(LayoutKind.Sequential)]
public  partial record struct ConditionalField
{

  public static readonly ConditionalField ZERO = new ConditionalField
  {
#if X11
    field = (int)0,

#endif
  };

  public static readonly ConditionalField ONE = new ConditionalField
  {
#if X11
    field = (int)1,

#endif
  };

#if X11
  public required int field;
#endif
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Normal
{

  public required int x;
  public required float y;
}
public static partial class Api
{

  // cbindgen: Skipped 'global_array_with_different_sizes', because it is a static extern.

  // cbindgen: Skipped 'global_array_with_different_sizes', because it is a static extern.

#if (PLATFORM_UNIX && X11)
  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(FooHandle a, C c);
#endif

#if (PLATFORM_WIN || M_32)
  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(BarHandle a, C c);
#endif

  [LibraryImport("library", EntryPoint = "cond")]
  public unsafe static partial void cond(ConditionalField a);

#if PLATFORM_WIN
  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial int foo();
#endif

#if PLATFORM_WIN
  [LibraryImport("library", EntryPoint = "bar")]
  public unsafe static partial void bar(Normal a);
#endif

}
