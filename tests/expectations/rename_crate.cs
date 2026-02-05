using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Foo
{

  public required int x;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct RenamedTy
{

  public required ulong y;
}

#if !DEFINE_FREEBSD
[StructLayout(LayoutKind.Sequential)]
public  partial record struct NoExternTy
{

  public required byte @field;
}
#endif

#if !DEFINE_FREEBSD
[StructLayout(LayoutKind.Sequential)]
public  partial record struct ContainsNoExternTy
{

  public required NoExternTy @field;
}
#endif

#if DEFINE_FREEBSD
[StructLayout(LayoutKind.Sequential)]
public  partial record struct ContainsNoExternTy
{

  public required ulong @field;
}
#endif
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(Foo a);

  [LibraryImport("library", EntryPoint = "renamed_func")]
  public unsafe static partial void renamed_func(RenamedTy a);

  [LibraryImport("library", EntryPoint = "no_extern_func")]
  public unsafe static partial void no_extern_func(ContainsNoExternTy a);

}
