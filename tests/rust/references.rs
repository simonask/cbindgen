use std::ffi::c_void;
use std::mem::MaybeUninit;
use std::ptr::NonNull;

#[repr(C)]
pub struct Point {
    x: i32,
    y: i32,
}

#[no_mangle]
pub extern "C" fn by_ref(shared: &Point, mutable: &mut Point, out: &mut MaybeUninit<Point>) {}

#[no_mangle]
pub extern "C" fn primitives(a: &i32, b: &mut u64, c: &mut MaybeUninit<f32>, _: &i8) {}

#[no_mangle]
pub extern "C" fn bools(a: &bool, b: &mut bool, c: &mut MaybeUninit<bool>) {}

#[no_mangle]
pub extern "C" fn nested(cursor: &mut *const u8, out: &mut MaybeUninit<*mut Point>, array: &[i32; 4]) {}

#[no_mangle]
pub unsafe extern "C" fn pointers(
    nullable: Option<&Point>,
    raw: *const Point,
    raw_mut: *mut Point,
    boxed: Box<Point>,
    non_null: NonNull<Point>,
    void: &c_void,
    callback: extern "C" fn(&Point, &mut i32),
) {
}

/// Returns the origin.
#[no_mangle]
pub extern "C" fn origin() -> &'static Point {}

#[no_mangle]
pub extern "C" fn first_mut<'a>(points: &'a mut Point, count: usize) -> &'a mut Point {}

#[deprecated]
#[no_mangle]
pub unsafe extern "C" fn flag(raw: *const bool, shared: &Point) -> &'static bool {}

#[no_mangle]
pub extern "C" fn maybe_origin() -> Option<&'static Point> {}

impl Point {
    #[no_mangle]
    pub extern "C" fn point_sum(&self) -> i32 {}

    #[no_mangle]
    pub extern "C" fn point_scale(&mut self, factor: i32) {}
}

#[no_mangle]
pub extern "C" fn interior(
    cell: &std::cell::Cell<i32>,
    unsafe_cell: &std::cell::UnsafeCell<Point>,
    mut_cell: &mut std::cell::Cell<i32>,
) {
}
