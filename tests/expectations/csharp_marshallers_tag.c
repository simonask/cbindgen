#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdlib.h>

/**
 * A borrowed UTF-8 string.
 */
struct FfiStr {
  const uint8_t *ptr;
  uintptr_t len;
};

typedef struct FfiStr Name;

/**
 * A borrowed slice.
 */
struct FfiSlice_u8 {
  const uint8_t *ptr;
  uintptr_t len;
};

/**
 * A borrowed slice.
 */
struct FfiSlice_u32 {
  const uint32_t *ptr;
  uintptr_t len;
};

struct Point {
  int32_t x;
  int32_t y;
};

/**
 * A borrowed slice.
 */
struct FfiSlice_Point {
  const struct Point *ptr;
  uintptr_t len;
};

/**
 * Fields are not marshalled.
 */
struct Holder {
  struct FfiStr name;
  struct FfiSlice_Point points;
};

uintptr_t str_len(struct FfiStr s);

struct FfiStr get_name(void);

bool is_named(Name name, bool strict);

uint64_t sum_bytes(struct FfiSlice_u8 bytes);

uint64_t sum_words(struct FfiSlice_u32 words);

struct Point sum_points(struct FfiSlice_Point points);

void by_pointer(const struct FfiStr *s, const struct FfiSlice_u8 *slice);

void holder(struct Holder holder, void (*callback)(struct FfiStr));
