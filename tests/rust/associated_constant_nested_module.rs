mod types {
    #[repr(C)]
    pub struct Foo { pub x: i32 }

    impl Foo {
        pub const SAME_MODULE: Foo = Foo { x: 1 };
    }
}

pub use types::Foo;

impl Foo {
    pub const PARENT_MODULE: Foo = Foo { x: 2 };
    pub const USES_OTHER: Foo = Foo::SAME_MODULE;
}

#[no_mangle]
pub extern "C" fn root(foo: Foo) {}
