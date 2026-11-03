#[repr(C)]
pub enum Foo {
    Bar(f32),
    Pair(i32, i32),
    Baz { x: i32, y: i32 },
    Empty,
}

impl Foo {
    pub const BAR2: Self = Self::Bar(2.0);
    pub const PAIR: Foo = Foo::Pair(1, -1);
    pub const BAZ: Foo = Foo::Baz { y: 2, x: 1 };
    /// Collides with the variant `Empty`.
    pub const EMPTY: Self = Self::Empty;
    pub const ALSO_BAR2: Foo = Foo::BAR2;
    pub const COUNT: u32 = 4;
}

#[repr(u8)]
pub enum Tagged {
    One(u8),
    Nested(Foo),
}

impl Tagged {
    pub const ONE: Self = Tagged::One(1);
    pub const NESTED: Self = Tagged::Nested(Foo::Baz { x: 3, y: 4 });
}

#[repr(C)]
pub enum Color {
    Red,
    Green,
}

impl Color {
    pub const DEFAULT: Color = Color::Green;
    pub const ALSO_DEFAULT: Color = Color::DEFAULT;
}

#[repr(C)]
pub struct Holder {
    foo: Foo,
    color: Color,
}

pub const HOLDER: Holder = Holder {
    foo: Foo::Pair(5, 6),
    color: Color::DEFAULT,
};

#[no_mangle]
pub extern "C" fn root(foo: Foo, tagged: Tagged, holder: Holder) {}
