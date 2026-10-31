#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdlib.h>

struct Foo {
  int32_t x;
};
#define Foo_FOO 1
#define Foo_BAR_BAZ (Foo_FOO + 1)
#define Foo_ORIGIN (Foo){ .x = Foo_BAR_BAZ }

void root(struct Foo foo);
