use std::marker::PhantomData;

/// A borrowed UTF-8 string.
#[repr(C)]
pub struct FfiStr<'a> {
    ptr: *const u8,
    len: usize,
    _marker: PhantomData<&'a str>,
}

/// A borrowed slice.
#[repr(C)]
pub struct FfiSlice<'a, T> {
    ptr: *const T,
    len: usize,
    _marker: PhantomData<&'a [T]>,
}

pub type Name<'a> = FfiStr<'a>;

#[repr(C)]
pub struct Point {
    x: i32,
    y: i32,
}

/// Fields are not marshalled.
#[repr(C)]
pub struct Holder<'a> {
    name: FfiStr<'a>,
    points: FfiSlice<'a, Point>,
}

#[no_mangle]
pub extern "C" fn str_len(s: FfiStr) -> usize {}

#[no_mangle]
pub extern "C" fn get_name() -> FfiStr<'static> {}

#[no_mangle]
pub extern "C" fn is_named(name: Name, strict: bool) -> bool {}

#[no_mangle]
pub extern "C" fn sum_bytes(bytes: FfiSlice<u8>) -> u64 {}

#[no_mangle]
pub extern "C" fn sum_words(words: FfiSlice<u32>) -> u64 {}

#[no_mangle]
pub extern "C" fn sum_points(points: FfiSlice<Point>) -> Point {}

#[no_mangle]
pub unsafe extern "C" fn by_pointer(s: *const FfiStr, slice: *const FfiSlice<u8>) {}

#[no_mangle]
pub extern "C" fn holder(holder: Holder, callback: extern "C" fn(FfiStr)) {}
