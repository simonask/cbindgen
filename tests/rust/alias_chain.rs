pub type C = i32;
pub type B = C;
pub type A = B;

#[no_mangle]
pub extern "C" fn root(a: A, b: B, c: C) {}
