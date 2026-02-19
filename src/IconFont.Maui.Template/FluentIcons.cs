using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Internals;


namespace IconFontTemplate;

/// <summary>
/// Provides helper APIs for the default icon font.
/// Uses the FontFamily from the first configured font (Regular by default).
/// </summary>
[Preserve(AllMembers = true)]
public static class FluentIcons
{
    /// <summary>
    /// The font family alias for the default (Regular) icon font style.
    /// </summary>
    public static string FontFamily => IconFontConfigs.Default.FontAlias;

    /// <summary>
    /// Creates a <see cref="FontImageSource"/> using glyphs emitted by the source generator.
    /// </summary>
    /// <param name="glyph">The glyph string, typically retrieved from <c>FluentIconsRegular</c> or <c>FluentIconsFilled</c>.</param>
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
