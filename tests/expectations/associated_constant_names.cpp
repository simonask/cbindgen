#include <cstdarg>
#include <cstdint>
#include <cstdlib>
#include <ostream>
#include <new>

struct Foo {
  int32_t x;
};
constexpr static const int32_t Foo_FOO = 1;
constexpr static const int32_t Foo_BAR_BAZ = (Foo_FOO + 1);
constexpr static const Foo Foo_ORIGIN = Foo{
  /* .x = */ Foo_BAR_BAZ
};

extern "C" {

void root(Foo foo);

}  // extern "C"
