#include <cstdarg>
#include <cstdint>
#include <cstdlib>
#include <ostream>
#include <new>

template<typename T = void>
struct Box;

template<typename T = void>
struct MaybeUninit;

struct Point {
  int32_t x;
  int32_t y;
};

extern "C" {

void by_ref(const Point *shared, Point *mutable_, MaybeUninit<Point> *out);

void primitives(const int32_t *a, uint64_t *b, MaybeUninit<float> *c, const int8_t*);

void bools(const bool *a, bool *b, MaybeUninit<bool> *c);

void nested(const uint8_t **cursor, MaybeUninit<Point*> *out, const int32_t (*array)[4]);

void pointers(const Point *nullable,
              const Point *raw,
              Point *raw_mut,
              Box<Point> boxed,
              Point *non_null,
              const void *void_,
              void (*callback)(const Point*, int32_t*));

/// Returns the origin.
const Point *origin();

Point *first_mut(Point *points, uintptr_t count);

const bool *flag(const bool *raw, const Point *shared);

const Point *maybe_origin();

int32_t point_sum(const Point *self);

void point_scale(Point *self, int32_t factor);

void interior(const int32_t *cell, const Point *unsafe_cell, int32_t *mut_cell);

}  // extern "C"
