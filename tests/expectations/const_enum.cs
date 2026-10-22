using System.Runtime.InteropServices;

public enum FillRule : byte
{
  Nonzero,
  Evenodd,
};

[StructLayout(LayoutKind.Sequential)]
public  partial record struct Style
{

  public static readonly FillRule DEFAULT_RULE = FillRule.Nonzero;

  public static ReadOnlySpan<FillRule> ALL_RULES => [FillRule.Nonzero, FillRule.Evenodd];

  public required FillRule rule;
}
public static partial class Api
{
  public static readonly FillRule DEFAULT_FILL_RULE = FillRule.Nonzero;
  public static ReadOnlySpan<FillRule> ALL_FILL_RULES => [FillRule.Nonzero, FillRule.Evenodd];

  [LibraryImport("library", EntryPoint = "root")]
  public unsafe static partial void root(FillRule rule, Style style);

}
