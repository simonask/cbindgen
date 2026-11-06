from libc.stdint cimport int8_t, int16_t, int32_t, int64_t, intptr_t
from libc.stdint cimport uint8_t, uint16_t, uint32_t, uint64_t, uintptr_t
cdef extern from *:
  ctypedef bint bool
  ctypedef struct va_list

cdef extern from *:

  cdef struct Point:
    int32_t x;
    int32_t y;

  void by_ref(const Point *shared, Point *mutable_, Point *out);

  void primitives(const int32_t *a, uint64_t *b, float *c, const int8_t*);

  void bools(const bool *a, bool *b, bool *c);

  void nested(const uint8_t **cursor, Point **out, const int32_t (*array)[4]);

  void pointers(const Point *nullable,
                const Point *raw,
                Point *raw_mut,
                Point *boxed,
                Point *non_null,
                const void *void_,
                void (*callback)(const Point*, int32_t*));

  # Returns the origin.
  const Point *origin();

  Point *first_mut(Point *points, uintptr_t count);

  const bool *flag(const bool *raw, const Point *shared);

  const Point *maybe_origin();

  int32_t point_sum(const Point *self);

  void point_scale(Point *self, int32_t factor);

  void interior(const int32_t *cell, const Point *unsafe_cell, int32_t *mut_cell);
