using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Internals;


namespace IconFontTemplate;

/// <summary>
/// Provides helper APIs for the default icon font.
/// Glyph constants are emitted at build time by the source generator into flat classes
/// such as <c>FluentIconsRegular</c> and <c>FluentIconsFilled</c> so that they can be
/// referenced directly via <c>{x:Static icons:FluentIconsRegular.Add24}</c> in XAML.
/// </summary>
[Preserve(AllMembers = true)]
public static class FluentIcons
{
    static FluentIcons() { }
    /// <summary>
    /// The font alias registered via MauiFont and buildTransitive targets.
    /// </summary>
    public static readonly string FontFamily = IconFontConfigs.Default.FontAlias;

    /// <summary>
    /// Creates a <see cref="FontImageSource"/> using glyphs emitted by the source generator.
    /// </summary>
    /// <param name="glyph">The glyph string, typically retrieved from <c>FluentIconsRegular</c>.</param>
    /// <param name="color">Optional foreground color; defaults to <see cref="Colors.Black"/>.</param>
    /// <param name="size">Icon size in device-independent units.</param>
    public static FontImageSource Create(string glyph, Color? color = null, double size = 24d)
    {
        return new FontImageSource
        {
            FontFamily = FontFamily,
            Glyph = glyph,
            Color = color ?? Colors.Black,
            Size = size
        };
    }
}
