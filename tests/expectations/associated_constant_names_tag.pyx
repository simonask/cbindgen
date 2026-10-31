from libc.stdint cimport int8_t, int16_t, int32_t, int64_t, intptr_t
from libc.stdint cimport uint8_t, uint16_t, uint32_t, uint64_t, uintptr_t
cdef extern from *:
  ctypedef bint bool
  ctypedef struct va_list

cdef extern from *:

  cdef struct Foo:
    int32_t x;
  const int32_t Foo_FOO # = 1
  const int32_t Foo_BAR_BAZ # = (Foo_FOO + 1)
  const Foo Foo_ORIGIN # = <Foo>{ Foo_BAR_BAZ }

  void root(Foo foo);
