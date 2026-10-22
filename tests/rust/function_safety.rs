#[no_mangle]
pub extern "C" fn safe_export(a: *const i32) {}

#[no_mangle]
pub unsafe extern "C" fn unsafe_export(a: *const i32) {}

extern "C" {
    pub fn legacy_import(a: i32);
}

unsafe extern "C" {
    pub safe fn safe_import(a: i32);
    pub unsafe fn unsafe_import(a: *const i32);
    pub fn default_import(a: i32);
}
