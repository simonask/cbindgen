#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdlib.h>

struct Point {
  int32_t x;
  int32_t y;
};

void by_ref(const struct Point *shared, struct Point *mutable_, struct Point *out);

void primitives(const int32_t *a, uint64_t *b, float *c, const int8_t*);

void bools(const bool *a, bool *b, bool *c);

void nested(const uint8_t **cursor, struct Point **out, const int32_t (*array)[4]);

void pointers(const struct Point *nullable,
              const struct Point *raw,
              struct Point *raw_mut,
              struct Point *boxed,
              struct Point *non_null,
              const void *void_,
              void (*callback)(const struct Point*, int32_t*));

/**
 * Returns the origin.
 */
const struct Point *origin(void);

struct Point *first_mut(struct Point *points, uintptr_t count);

const bool *flag(const bool *raw, const struct Point *shared);

const struct Point *maybe_origin(void);

int32_t point_sum(const struct Point *self);

void point_scale(struct Point *self, int32_t factor);

void interior(const int32_t *cell, const struct Point *unsafe_cell, int32_t *mut_cell);
