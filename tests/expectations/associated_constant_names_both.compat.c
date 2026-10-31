#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdlib.h>

typedef struct Foo {
  int32_t x;
} Foo;
#define Foo_FOO 1
#define Foo_BAR_BAZ (Foo_FOO + 1)
#define Foo_ORIGIN (Foo){ .x = Foo_BAR_BAZ }

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

void root(struct Foo foo);

#ifdef __cplusplus
}  // extern "C"
#endif  // __cplusplus
