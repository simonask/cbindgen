using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public  partial record struct StylePoint_i32
{

  public required int x;
  public required int y;
}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct StylePoint_f32
{

  public required float x;
  public required float y;
}

[StructLayout(LayoutKind.Explicit)]
public struct StyleFoo_i32
{
  public enum Tag : byte
  {
    Foo_i32,
    Bar_i32,
    Baz_i32,
    Bazz_i32,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private StyleFoo_Body_i32 foo;
  [FieldOffset(0)]
  private StyleBar_Body_i32 bar;
  [FieldOffset(0)]
  private StyleBaz_Body_i32 baz;

  public static StyleFoo_i32 Foo_i32(int x, StylePoint_i32 y, StylePoint_f32 z)
     => new(new StyleFoo_Body_i32
      {
        x = x,
        y = y,
        z = z,

      });
  public static StyleFoo_i32 Bar_i32(int bar)
     => new(new StyleBar_Body_i32
      {
        bar = bar,

      });
  public static StyleFoo_i32 Baz_i32(StylePoint_i32 baz)
     => new(new StyleBaz_Body_i32
      {
        baz = baz,

      });
  public static StyleFoo_i32 Bazz_i32() => new() { _tag = Tag.Bazz_i32 };

  public StyleFoo_i32(StyleFoo_Body_i32 foo)
  {
    this.foo = foo;
  }
  public StyleFoo_i32(StyleBar_Body_i32 bar)
  {
    this.bar = bar;
  }
  public StyleFoo_i32(StyleBaz_Body_i32 baz)
  {
    this.baz = baz;
  }


  public static implicit operator StyleFoo_i32(StyleFoo_Body_i32 value) => new(value);
  public static implicit operator StyleFoo_i32(StyleBar_Body_i32 value) => new(value);
  public static implicit operator StyleFoo_i32(StyleBaz_Body_i32 value) => new(value);

  public readonly bool IsFoo_i32 => _tag == Tag.Foo_i32;
  public readonly bool IsBar_i32 => _tag == Tag.Bar_i32;
  public readonly bool IsBaz_i32 => _tag == Tag.Baz_i32;
  public readonly bool IsBazz_i32 => _tag == Tag.Bazz_i32;

  public readonly StyleFoo_Body_i32? AsFoo_i32 => _tag == Tag.Foo_i32 ? foo : null;
  public readonly StyleBar_Body_i32? AsBar_i32 => _tag == Tag.Bar_i32 ? bar : null;
  public readonly StyleBaz_Body_i32? AsBaz_i32 => _tag == Tag.Baz_i32 ? baz : null;


  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleFoo_Body_i32()
  {
    private readonly Tag _tag = Tag.Foo_i32;
    public required int x;
    public required StylePoint_i32 y;
    public required StylePoint_f32 z;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBar_Body_i32()
  {
    private readonly Tag _tag = Tag.Bar_i32;
    public required int bar;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBaz_Body_i32()
  {
    private readonly Tag _tag = Tag.Baz_i32;
    public required StylePoint_i32 baz;
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct StyleBar_i32
{
  public enum Tag
  {
    Bar1_i32,
    Bar2_i32,
    Bar3_i32,
    Bar4_i32,
  };
  private Tag _tag;
  private StyleBar_i32_Data _data;

  public unsafe static StyleBar_i32 Bar1_i32(int x, StylePoint_i32 y, StylePoint_f32 z, delegate* unmanaged[Cdecl]<int, int> u)
     => new(new StyleBar1_Body_i32
      {
        x = x,
        y = y,
        z = z,
        u = u,

      });
  public static StyleBar_i32 Bar2_i32(int bar2)
     => new(new StyleBar2_Body_i32
      {
        bar2 = bar2,

      });
  public static StyleBar_i32 Bar3_i32(StylePoint_i32 bar3)
     => new(new StyleBar3_Body_i32
      {
        bar3 = bar3,

      });
  public static StyleBar_i32 Bar4_i32() => new() { _tag = Tag.Bar4_i32 };

  public StyleBar_i32(StyleBar1_Body_i32 bar1)
  {
    _tag = Tag.Bar1_i32;
    _data.bar1 = bar1;
  }
  public StyleBar_i32(StyleBar2_Body_i32 bar2)
  {
    _tag = Tag.Bar2_i32;
    _data.bar2 = bar2;
  }
  public StyleBar_i32(StyleBar3_Body_i32 bar3)
  {
    _tag = Tag.Bar3_i32;
    _data.bar3 = bar3;
  }


  public static implicit operator StyleBar_i32(StyleBar1_Body_i32 value) => new(value);
  public static implicit operator StyleBar_i32(StyleBar2_Body_i32 value) => new(value);
  public static implicit operator StyleBar_i32(StyleBar3_Body_i32 value) => new(value);

  public readonly bool IsBar1_i32 => _tag == Tag.Bar1_i32;
  public readonly bool IsBar2_i32 => _tag == Tag.Bar2_i32;
  public readonly bool IsBar3_i32 => _tag == Tag.Bar3_i32;
  public readonly bool IsBar4_i32 => _tag == Tag.Bar4_i32;

  public readonly StyleBar1_Body_i32? AsBar1_i32 => _tag == Tag.Bar1_i32 ? _data.bar1 : null;
  public readonly StyleBar2_Body_i32? AsBar2_i32 => _tag == Tag.Bar2_i32 ? _data.bar2 : null;
  public readonly StyleBar3_Body_i32? AsBar3_i32 => _tag == Tag.Bar3_i32 ? _data.bar3 : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct StyleBar_i32_Data
  {
    [FieldOffset(0)]
    public StyleBar1_Body_i32 bar1;
    [FieldOffset(0)]
    public StyleBar2_Body_i32 bar2;
    [FieldOffset(0)]
    public StyleBar3_Body_i32 bar3;
  }
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct StyleBar1_Body_i32()
  {
    public required int x;
    public required StylePoint_i32 y;
    public required StylePoint_f32 z;
    public required delegate* unmanaged[Cdecl]<int, int> u;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBar2_Body_i32()
  {
    public required int bar2;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBar3_Body_i32()
  {
    public required StylePoint_i32 bar3;
  }


}

[StructLayout(LayoutKind.Sequential)]
public  partial record struct StylePoint_u32
{

  public required uint x;
  public required uint y;
}

[StructLayout(LayoutKind.Sequential)]
public struct StyleBar_u32
{
  public enum Tag
  {
    Bar1_u32,
    Bar2_u32,
    Bar3_u32,
    Bar4_u32,
  };
  private Tag _tag;
  private StyleBar_u32_Data _data;

  public unsafe static StyleBar_u32 Bar1_u32(int x, StylePoint_u32 y, StylePoint_f32 z, delegate* unmanaged[Cdecl]<int, int> u)
     => new(new StyleBar1_Body_u32
      {
        x = x,
        y = y,
        z = z,
        u = u,

      });
  public static StyleBar_u32 Bar2_u32(uint bar2)
     => new(new StyleBar2_Body_u32
      {
        bar2 = bar2,

      });
  public static StyleBar_u32 Bar3_u32(StylePoint_u32 bar3)
     => new(new StyleBar3_Body_u32
      {
        bar3 = bar3,

      });
  public static StyleBar_u32 Bar4_u32() => new() { _tag = Tag.Bar4_u32 };

  public StyleBar_u32(StyleBar1_Body_u32 bar1)
  {
    _tag = Tag.Bar1_u32;
    _data.bar1 = bar1;
  }
  public StyleBar_u32(StyleBar2_Body_u32 bar2)
  {
    _tag = Tag.Bar2_u32;
    _data.bar2 = bar2;
  }
  public StyleBar_u32(StyleBar3_Body_u32 bar3)
  {
    _tag = Tag.Bar3_u32;
    _data.bar3 = bar3;
  }


  public static implicit operator StyleBar_u32(StyleBar1_Body_u32 value) => new(value);
  public static implicit operator StyleBar_u32(StyleBar2_Body_u32 value) => new(value);
  public static implicit operator StyleBar_u32(StyleBar3_Body_u32 value) => new(value);

  public readonly bool IsBar1_u32 => _tag == Tag.Bar1_u32;
  public readonly bool IsBar2_u32 => _tag == Tag.Bar2_u32;
  public readonly bool IsBar3_u32 => _tag == Tag.Bar3_u32;
  public readonly bool IsBar4_u32 => _tag == Tag.Bar4_u32;

  public readonly StyleBar1_Body_u32? AsBar1_u32 => _tag == Tag.Bar1_u32 ? _data.bar1 : null;
  public readonly StyleBar2_Body_u32? AsBar2_u32 => _tag == Tag.Bar2_u32 ? _data.bar2 : null;
  public readonly StyleBar3_Body_u32? AsBar3_u32 => _tag == Tag.Bar3_u32 ? _data.bar3 : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct StyleBar_u32_Data
  {
    [FieldOffset(0)]
    public StyleBar1_Body_u32 bar1;
    [FieldOffset(0)]
    public StyleBar2_Body_u32 bar2;
    [FieldOffset(0)]
    public StyleBar3_Body_u32 bar3;
  }
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct StyleBar1_Body_u32()
  {
    public required int x;
    public required StylePoint_u32 y;
    public required StylePoint_f32 z;
    public required delegate* unmanaged[Cdecl]<int, int> u;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBar2_Body_u32()
  {
    public required uint bar2;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBar3_Body_u32()
  {
    public required StylePoint_u32 bar3;
  }


}

[StructLayout(LayoutKind.Explicit)]
public struct StyleBaz
{
  public enum Tag : byte
  {
    Baz1,
    Baz2,
    Baz3,
  };
  [FieldOffset(0)]
  private Tag _tag;
  [FieldOffset(0)]
  private StyleBaz1_Body baz1;
  [FieldOffset(0)]
  private StyleBaz2_Body baz2;

  public static StyleBaz Baz1(StyleBar_u32 baz1)
     => new(new StyleBaz1_Body
      {
        baz1 = baz1,

      });
  public static StyleBaz Baz2(StylePoint_i32 baz2)
     => new(new StyleBaz2_Body
      {
        baz2 = baz2,

      });
  public static StyleBaz Baz3() => new() { _tag = Tag.Baz3 };

  public StyleBaz(StyleBaz1_Body baz1)
  {
    this.baz1 = baz1;
  }
  public StyleBaz(StyleBaz2_Body baz2)
  {
    this.baz2 = baz2;
  }


  public static implicit operator StyleBaz(StyleBaz1_Body value) => new(value);
  public static implicit operator StyleBaz(StyleBaz2_Body value) => new(value);

  public readonly bool IsBaz1 => _tag == Tag.Baz1;
  public readonly bool IsBaz2 => _tag == Tag.Baz2;
  public readonly bool IsBaz3 => _tag == Tag.Baz3;

  public readonly StyleBaz1_Body? AsBaz1 => _tag == Tag.Baz1 ? baz1 : null;
  public readonly StyleBaz2_Body? AsBaz2 => _tag == Tag.Baz2 ? baz2 : null;


  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBaz1_Body()
  {
    private readonly Tag _tag = Tag.Baz1;
    public required StyleBar_u32 baz1;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleBaz2_Body()
  {
    private readonly Tag _tag = Tag.Baz2;
    public required StylePoint_i32 baz2;
  }

}

[StructLayout(LayoutKind.Sequential)]
public struct StyleTaz
{
  public enum Tag : byte
  {
    Taz1,
    Taz2,
    Taz3,
  };
  private Tag _tag;
  private StyleTaz_Data _data;

  public static StyleTaz Taz1(StyleBar_u32 taz1)
     => new(new StyleTaz1_Body
      {
        taz1 = taz1,

      });
  public static StyleTaz Taz2(StyleBaz taz2)
     => new(new StyleTaz2_Body
      {
        taz2 = taz2,

      });
  public static StyleTaz Taz3() => new() { _tag = Tag.Taz3 };

  public StyleTaz(StyleTaz1_Body taz1)
  {
    _tag = Tag.Taz1;
    _data.taz1 = taz1;
  }
  public StyleTaz(StyleTaz2_Body taz2)
  {
    _tag = Tag.Taz2;
    _data.taz2 = taz2;
  }


  public static implicit operator StyleTaz(StyleTaz1_Body value) => new(value);
  public static implicit operator StyleTaz(StyleTaz2_Body value) => new(value);

  public readonly bool IsTaz1 => _tag == Tag.Taz1;
  public readonly bool IsTaz2 => _tag == Tag.Taz2;
  public readonly bool IsTaz3 => _tag == Tag.Taz3;

  public readonly StyleTaz1_Body? AsTaz1 => _tag == Tag.Taz1 ? _data.taz1 : null;
  public readonly StyleTaz2_Body? AsTaz2 => _tag == Tag.Taz2 ? _data.taz2 : null;

  [StructLayout(LayoutKind.Explicit)]
  private struct StyleTaz_Data
  {
    [FieldOffset(0)]
    public StyleTaz1_Body taz1;
    [FieldOffset(0)]
    public StyleTaz2_Body taz2;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleTaz1_Body()
  {
    public required StyleBar_u32 taz1;
  }
  [StructLayout(LayoutKind.Sequential)]
  public record struct StyleTaz2_Body()
  {
    public required StyleBaz taz2;
  }


}
public static partial class Api
{

  [LibraryImport("library", EntryPoint = "foo")]
  public unsafe static partial void
  foo(StyleFoo_i32* foo,
    StyleBar_i32* bar,
    StyleBaz* baz,
    StyleTaz* taz);

}
