#include <cstdarg>
#include <cstdint>
#include <cstdlib>
#include <ostream>
#include <new>

/// A borrowed UTF-8 string.
struct FfiStr {
  const uint8_t *ptr;
  uintptr_t len;
};

using Name = FfiStr;

/// A borrowed slice.
template<typename T>
struct FfiSlice {
  const T *ptr;
  uintptr_t len;
};

struct Point {
  int32_t x;
  int32_t y;
};

/// Fields are not marshalled.
struct Holder {
  FfiStr name;
  FfiSlice<Point> points;
};

extern "C" {

uintptr_t str_len(FfiStr s);

FfiStr get_name();

bool is_named(Name name, bool strict);

uint64_t sum_bytes(FfiSlice<uint8_t> bytes);

uint64_t sum_words(FfiSlice<uint32_t> words);

Point sum_points(FfiSlice<Point> points);

void by_pointer(const FfiStr *s, const FfiSlice<uint8_t> *slice);

void holder(Holder holder, void (*callback)(FfiStr));

}  // extern "C"
