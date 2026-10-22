from libc.stdint cimport int8_t, int16_t, int32_t, int64_t, intptr_t
from libc.stdint cimport uint8_t, uint16_t, uint32_t, uint64_t, uintptr_t
cdef extern from *:
  ctypedef bint bool
  ctypedef struct va_list

cdef extern from *:

  # Constants shared by multiple CSS Box Alignment properties
  #
  # These constants match Gecko's `NS_STYLE_ALIGN_*` constants.
  ctypedef uint8_t AlignFlags;
  # 'auto'
  const AlignFlags AlignFlags_AUTO # = <uint8_t>0
  # 'normal'
  const AlignFlags AlignFlags_NORMAL # = <uint8_t>1
  # 'start'
  const AlignFlags AlignFlags_START # = <uint8_t>(1 << 1)
  # 'end'
  const AlignFlags AlignFlags_END # = <uint8_t>(1 << 2)
  const AlignFlags AlignFlags_ALIAS # = <uint8_t>AlignFlags_END
  # 'flex-start'
  const AlignFlags AlignFlags_FLEX_START # = <uint8_t>(1 << 3)
  const AlignFlags AlignFlags_MIXED # = <uint8_t>(((1 << 4) | AlignFlags_FLEX_START) | AlignFlags_END)
  const AlignFlags AlignFlags_MIXED_SELF # = <uint8_t>(((1 << 5) | AlignFlags_FLEX_START) | AlignFlags_END)

  ctypedef uint32_t DebugFlags;
  # Flag with the topmost bit set of the u32
  const DebugFlags DebugFlags_BIGGEST_ALLOWED # = <uint32_t>(1 << 31)

  ctypedef uint64_t LargeFlags;
  # Flag with a very large shift that usually would be narrowed.
  const LargeFlags LargeFlags_LARGE_SHIFT # = <uint64_t>(1ull << 44)
  const LargeFlags LargeFlags_INVERTED # = <uint64_t>~LargeFlags_LARGE_SHIFT

  ctypedef uint32_t OutOfLine;
  const OutOfLine OutOfLine_A # = <uint32_t>1
  const OutOfLine OutOfLine_B # = <uint32_t>2
  const OutOfLine OutOfLine_AB # = <uint32_t>(OutOfLine_A | OutOfLine_B)

  void root(AlignFlags flags,
            DebugFlags bigger_flags,
            LargeFlags largest_flags,
            OutOfLine out_of_line);
