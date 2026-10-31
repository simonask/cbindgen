#[repr(C)]
pub struct Foo {
    x: i32,
}

impl Foo {
    pub const FOO: i32 = 1;
    pub const BAR_BAZ: i32 = Foo::FOO + 1;
    pub const ORIGIN: Foo = Foo { x: Foo::BAR_BAZ };
}

#[no_mangle]
pub extern "C" fn root(foo: Foo) {}
