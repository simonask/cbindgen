from libc.stdint cimport int8_t, int16_t, int32_t, int64_t, intptr_t
from libc.stdint cimport uint8_t, uint16_t, uint32_t, uint64_t, uintptr_t
cdef extern from *:
  ctypedef bint bool
  ctypedef struct va_list

cdef extern from *:

  # A borrowed UTF-8 string.
  ctypedef struct FfiStr:
    const uint8_t *ptr;
    uintptr_t len;

  ctypedef FfiStr Name;

  # A borrowed slice.
  ctypedef struct FfiSlice_u8:
    const uint8_t *ptr;
    uintptr_t len;

  # A borrowed slice.
  ctypedef struct FfiSlice_u32:
    const uint32_t *ptr;
    uintptr_t len;

  ctypedef struct Point:
    int32_t x;
    int32_t y;

  # A borrowed slice.
  ctypedef struct FfiSlice_Point:
    const Point *ptr;
    uintptr_t len;

  # Fields are not marshalled.
  ctypedef struct Holder:
    FfiStr name;
    FfiSlice_Point points;

  uintptr_t str_len(FfiStr s);

  FfiStr get_name();

  bool is_named(Name name, bool strict);

  uint64_t sum_bytes(FfiSlice_u8 bytes);

  uint64_t sum_words(FfiSlice_u32 words);

  Point sum_points(FfiSlice_Point points);

  void by_pointer(const FfiStr *s, const FfiSlice_u8 *slice);

  void holder(Holder holder, void (*callback)(FfiStr));
