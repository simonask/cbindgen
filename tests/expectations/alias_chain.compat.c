#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdlib.h>

typedef int32_t C;

typedef C B;

typedef B A;

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

void root(A a, B b, C c);

#ifdef __cplusplus
}  // extern "C"
#endif  // __cplusplus
