#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdlib.h>

/**
 * A borrowed UTF-8 string.
 */
typedef struct FfiStr {
  const uint8_t *ptr;
  uintptr_t len;
} FfiStr;

typedef struct FfiStr Name;

/**
 * A borrowed slice.
 */
typedef struct FfiSlice_u8 {
  const uint8_t *ptr;
  uintptr_t len;
} FfiSlice_u8;

/**
 * A borrowed slice.
 */
typedef struct FfiSlice_u32 {
  const uint32_t *ptr;
  uintptr_t len;
} FfiSlice_u32;

typedef struct Point {
  int32_t x;
  int32_t y;
} Point;

/**
 * A borrowed slice.
 */
typedef struct FfiSlice_Point {
  const struct Point *ptr;
  uintptr_t len;
} FfiSlice_Point;

/**
 * Fields are not marshalled.
 */
typedef struct Holder {
  struct FfiStr name;
  struct FfiSlice_Point points;
} Holder;

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

uintptr_t str_len(struct FfiStr s);

struct FfiStr get_name(void);

bool is_named(Name name, bool strict);

uint64_t sum_bytes(struct FfiSlice_u8 bytes);

uint64_t sum_words(struct FfiSlice_u32 words);

struct Point sum_points(struct FfiSlice_Point points);

void by_pointer(const struct FfiStr *s, const struct FfiSlice_u8 *slice);

void holder(struct Holder holder, void (*callback)(struct FfiStr));

#ifdef __cplusplus
}  // extern "C"
#endif  // __cplusplus
