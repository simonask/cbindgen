#include <cstdarg>
#include <cstdint>
#include <cstdlib>
#include <ostream>
#include <new>

using C = int32_t;

using B = C;

using A = B;

extern "C" {

void root(A a, B b, C c);

}  // extern "C"
