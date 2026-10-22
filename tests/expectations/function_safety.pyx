from libc.stdint cimport int8_t, int16_t, int32_t, int64_t, intptr_t
from libc.stdint cimport uint8_t, uint16_t, uint32_t, uint64_t, uintptr_t
cdef extern from *:
  ctypedef bint bool
  ctypedef struct va_list

cdef extern from *:

  void safe_export(const int32_t *a);

  void unsafe_export(const int32_t *a);

  extern void legacy_import(int32_t a);

  extern void safe_import(int32_t a);

  extern void unsafe_import(const int32_t *a);

  extern void default_import(int32_t a);
