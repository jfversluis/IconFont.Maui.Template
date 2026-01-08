using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Internals;


namespace IconFontTemplate;

/// <summary>
/// Provides helper APIs and glyph constants for the supplied icon font.
/// Glyph constants are emitted at build time by the source generator based on the font file embedded in this package.
/// </summary>
[Preserve(AllMembers = true)]
public static partial class FluentIcons
{
    static FluentIcons() { }
    /// <summary>
    /// The font alias registered via MauiFont and buildTransitive targets.
    /// </summary>
    public static readonly string FontFamily = IconFontConfigs.Default.FontAlias;

    /// <summary>
    /// Regular style glyphs generated at build time.
    /// </summary>
    public static partial class Regular
    {
    }

    /// <summary>
    /// Creates a <see cref="FontImageSource"/> using glyphs emitted by the source generator.
    /// </summary>
    /// <param name="glyph">The glyph string, typically retrieved from <see cref="Regular"/>.</param>
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
