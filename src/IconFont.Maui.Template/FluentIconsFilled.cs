using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Internals;
using System.Diagnostics.CodeAnalysis;

namespace IconFontTemplate;

[Preserve(AllMembers = true)]
public static partial class FluentIconsFilled
{
    static FluentIconsFilled() { }
    // Font alias for filled font (from IconFontConfigs)
    public static readonly string FontFamily = IconFontConfigs.All.First(x => x.ClassName == "FluentIconsFilled").FontAlias;

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
