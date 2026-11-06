#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdlib.h>

typedef struct {
  int32_t x;
  int32_t y;
} Point;

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

/**
 * Returns the origin.
 */
const Point *origin(void);

Point *first_mut(Point *points, uintptr_t count);

const bool *flag(const bool *raw, const Point *shared);

const Point *maybe_origin(void);

int32_t point_sum(const Point *self);

void point_scale(Point *self, int32_t factor);

void interior(const int32_t *cell, const Point *unsafe_cell, int32_t *mut_cell);
