#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdlib.h>

typedef struct Foo {
  int32_t x;
} Foo;
#define Foo_SAME_MODULE (Foo){ .x = 1 }

#define Foo_PARENT_MODULE (Foo){ .x = 2 }

#define Foo_USES_OTHER Foo_SAME_MODULE

void root(struct Foo foo);
