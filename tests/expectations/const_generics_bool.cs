using System.Runtime.InteropServices;
using Str = nint;
using MySet = HashTable_Str__c_char__false;
using SetCallback = nint;
using MapCallback = nint;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct HashTable_Str__c_char__false
{

  public required nuint num_buckets;
  public required nuint capacity;
  public required byte* occupied;
  public required Str* keys;
  public required byte* vals;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial  struct HashTable_Str__u64__true
{

  public required nuint num_buckets;
  public required nuint capacity;
  public required byte* occupied;
  public required Str* keys;
  public required ulong* vals;
}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "new_set")]
  public unsafe static partial MySet* new_set();

  [LibraryImport("library", EntryPoint = "set_for_each")]
  public unsafe static partial void set_for_each(MySet* set, SetCallback callback);

  [LibraryImport("library", EntryPoint = "new_map")]
  public unsafe static partial HashTable_Str__u64__true* new_map();

  [LibraryImport("library", EntryPoint = "map_for_each")]
  public unsafe static partial void
  map_for_each(HashTable_Str__u64__true* map,
  MapCallback callback);

}
