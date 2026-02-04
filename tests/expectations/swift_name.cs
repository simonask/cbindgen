using System.Runtime.InteropServices;

public readonly struct Opaque
{
  private readonly byte _opaque;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct SelfTypeTestStruct
{

  public required byte times;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct PointerToOpaque
{

  public required Opaque* ptr;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "rust_print_hello_world")]
  public unsafe static partial void rust_print_hello_world();

  [LibraryImport("library", EntryPoint = "SelfTypeTestStruct_should_exist_ref")]
  public unsafe static partial void SelfTypeTestStruct_should_exist_ref(SelfTypeTestStruct* self);

  [LibraryImport("library", EntryPoint = "SelfTypeTestStruct_should_exist_ref_mut")]
  public unsafe static partial void
  SelfTypeTestStruct_should_exist_ref_mut(SelfTypeTestStruct* self);

  [LibraryImport("library", EntryPoint = "SelfTypeTestStruct_should_not_exist_box")]
  public unsafe static partial void
  SelfTypeTestStruct_should_not_exist_box(SelfTypeTestStruct* self);

  [LibraryImport("library", EntryPoint = "SelfTypeTestStruct_should_not_exist_return_box")]
  public unsafe static partial SelfTypeTestStruct* SelfTypeTestStruct_should_not_exist_return_box();

  [LibraryImport("library", EntryPoint = "SelfTypeTestStruct_should_exist_annotated_self")]
  public unsafe static partial void
  SelfTypeTestStruct_should_exist_annotated_self(SelfTypeTestStruct self);

  [LibraryImport("library", EntryPoint = "SelfTypeTestStruct_should_exist_annotated_mut_self")]
  public unsafe static partial void
  SelfTypeTestStruct_should_exist_annotated_mut_self(SelfTypeTestStruct self);

  [LibraryImport("library", EntryPoint = "SelfTypeTestStruct_should_exist_annotated_by_name")]
  public unsafe static partial void
  SelfTypeTestStruct_should_exist_annotated_by_name(SelfTypeTestStruct self);

  [LibraryImport("library", EntryPoint = "SelfTypeTestStruct_should_exist_annotated_mut_by_name")]
  public unsafe static partial void
  SelfTypeTestStruct_should_exist_annotated_mut_by_name(SelfTypeTestStruct self);

  [LibraryImport("library", EntryPoint = "SelfTypeTestStruct_should_exist_unannotated")]
  public unsafe static partial void
  SelfTypeTestStruct_should_exist_unannotated(SelfTypeTestStruct self);

  [LibraryImport("library", EntryPoint = "SelfTypeTestStruct_should_exist_mut_unannotated")]
  public unsafe static partial void
  SelfTypeTestStruct_should_exist_mut_unannotated(SelfTypeTestStruct self);

  [LibraryImport("library", EntryPoint = "free_function_should_exist_ref")]
  public unsafe static partial void free_function_should_exist_ref(SelfTypeTestStruct* test_struct);

  [LibraryImport("library", EntryPoint = "free_function_should_exist_ref_mut")]
  public unsafe static partial void
  free_function_should_exist_ref_mut(SelfTypeTestStruct* test_struct);

  [LibraryImport("library", EntryPoint = "unnamed_argument")]
  public unsafe static partial void unnamed_argument(SelfTypeTestStruct* arg0);

  [LibraryImport("library", EntryPoint = "free_function_should_not_exist_box")]
  public unsafe static partial void free_function_should_not_exist_box(SelfTypeTestStruct* boxed);

  [LibraryImport("library", EntryPoint = "free_function_should_exist_annotated_by_name")]
  public unsafe static partial void
  free_function_should_exist_annotated_by_name(SelfTypeTestStruct test_struct);

  [LibraryImport("library", EntryPoint = "free_function_should_exist_annotated_mut_by_name")]
  public unsafe static partial void
  free_function_should_exist_annotated_mut_by_name(SelfTypeTestStruct test_struct);

  [LibraryImport("library", EntryPoint = "PointerToOpaque_create")]
  public unsafe static partial PointerToOpaque PointerToOpaque_create(byte times);

  [LibraryImport("library", EntryPoint = "PointerToOpaque_sayHello")]
  public unsafe static partial void PointerToOpaque_sayHello(PointerToOpaque self);

}
