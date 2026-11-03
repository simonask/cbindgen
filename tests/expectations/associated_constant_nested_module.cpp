#include <cstdarg>
#include <cstdint>
#include <cstdlib>
#include <ostream>
#include <new>

struct Foo {
  int32_t x;
};
constexpr static const Foo Foo_SAME_MODULE = Foo{
  /* .x = */ 1
};

constexpr static const Foo Foo_PARENT_MODULE = Foo{
  /* .x = */ 2
};

constexpr static const Foo Foo_USES_OTHER = Foo_SAME_MODULE;

extern "C" {

void root(Foo foo);

}  // extern "C"
