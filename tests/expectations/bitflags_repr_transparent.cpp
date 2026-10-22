#include <cstdarg>
#include <cstdint>
#include <cstdlib>
#include <ostream>
#include <new>

/// Constants shared by multiple CSS Box Alignment properties
///
/// These constants match Gecko's `NS_STYLE_ALIGN_*` constants.
using AlignFlags = uint8_t;
/// 'auto'
constexpr static const AlignFlags AlignFlags_AUTO = (uint8_t)0;
/// 'normal'
constexpr static const AlignFlags AlignFlags_NORMAL = (uint8_t)1;
/// 'start'
constexpr static const AlignFlags AlignFlags_START = (uint8_t)(1 << 1);
/// 'end'
constexpr static const AlignFlags AlignFlags_END = (uint8_t)(1 << 2);
constexpr static const AlignFlags AlignFlags_ALIAS = (uint8_t)AlignFlags_END;
/// 'flex-start'
constexpr static const AlignFlags AlignFlags_FLEX_START = (uint8_t)(1 << 3);
constexpr static const AlignFlags AlignFlags_MIXED = (uint8_t)(((1 << 4) | AlignFlags_FLEX_START) | AlignFlags_END);
constexpr static const AlignFlags AlignFlags_MIXED_SELF = (uint8_t)(((1 << 5) | AlignFlags_FLEX_START) | AlignFlags_END);

using DebugFlags = uint32_t;
/// Flag with the topmost bit set of the u32
constexpr static const DebugFlags DebugFlags_BIGGEST_ALLOWED = (uint32_t)(1 << 31);

using LargeFlags = uint64_t;
/// Flag with a very large shift that usually would be narrowed.
constexpr static const LargeFlags LargeFlags_LARGE_SHIFT = (uint64_t)(1ull << 44);
constexpr static const LargeFlags LargeFlags_INVERTED = (uint64_t)~LargeFlags_LARGE_SHIFT;

using OutOfLine = uint32_t;
constexpr static const OutOfLine OutOfLine_A = (uint32_t)1;
constexpr static const OutOfLine OutOfLine_B = (uint32_t)2;
constexpr static const OutOfLine OutOfLine_AB = (uint32_t)(OutOfLine_A | OutOfLine_B);

extern "C" {

void root(AlignFlags flags,
          DebugFlags bigger_flags,
          LargeFlags largest_flags,
          OutOfLine out_of_line);

}  // extern "C"
