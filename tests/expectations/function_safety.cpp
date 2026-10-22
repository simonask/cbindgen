#include <cstdarg>
#include <cstdint>
#include <cstdlib>
#include <ostream>
#include <new>

extern "C" {

void safe_export(const int32_t *a);

void unsafe_export(const int32_t *a);

extern void legacy_import(int32_t a);

extern void safe_import(int32_t a);

extern void unsafe_import(const int32_t *a);

extern void default_import(int32_t a);

}  // extern "C"
