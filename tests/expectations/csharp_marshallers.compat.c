#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdlib.h>

/**
 * A borrowed UTF-8 string.
 */
typedef struct {
  const uint8_t *ptr;
  uintptr_t len;
} FfiStr;

typedef FfiStr Name;

/**
 * A borrowed slice.
 */
typedef struct {
  const uint8_t *ptr;
  uintptr_t len;
} FfiSlice_u8;

/**
 * A borrowed slice.
 */
typedef struct {
  const uint32_t *ptr;
  uintptr_t len;
} FfiSlice_u32;

typedef struct {
  int32_t x;
  int32_t y;
} Point;

/**
 * A borrowed slice.
 */
typedef struct {
  const Point *ptr;
  uintptr_t len;
} FfiSlice_Point;

/**
 * Fields are not marshalled.
 */
typedef struct {
  FfiStr name;
  FfiSlice_Point points;
} Holder;

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

uintptr_t str_len(FfiStr s);

FfiStr get_name(void);

bool is_named(Name name, bool strict);

uint64_t sum_bytes(FfiSlice_u8 bytes);

uint64_t sum_words(FfiSlice_u32 words);

Point sum_points(FfiSlice_Point points);

void by_pointer(const FfiStr *s, const FfiSlice_u8 *slice);

void holder(Holder holder, void (*callback)(FfiStr));

#ifdef __cplusplus
}  // extern "C"
#endif  // __cplusplus
