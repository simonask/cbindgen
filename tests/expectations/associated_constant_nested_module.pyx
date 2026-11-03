from libc.stdint cimport int8_t, int16_t, int32_t, int64_t, intptr_t
from libc.stdint cimport uint8_t, uint16_t, uint32_t, uint64_t, uintptr_t
cdef extern from *:
  ctypedef bint bool
  ctypedef struct va_list

cdef extern from *:

  ctypedef struct Foo:
    int32_t x;
  const Foo Foo_SAME_MODULE # = <Foo>{ 1 }

  const Foo Foo_PARENT_MODULE # = <Foo>{ 2 }

  const Foo Foo_USES_OTHER # = Foo_SAME_MODULE

  void root(Foo foo);
