#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdlib.h>

typedef struct {
  int32_t x;
} Foo;
#define Foo_SAME_MODULE (Foo){ .x = 1 }

#define Foo_PARENT_MODULE (Foo){ .x = 2 }

#define Foo_USES_OTHER Foo_SAME_MODULE

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

void root(Foo foo);

#ifdef __cplusplus
}  // extern "C"
#endif  // __cplusplus
