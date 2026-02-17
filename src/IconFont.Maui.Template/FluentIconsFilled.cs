using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Internals;

namespace IconFontTemplate;

[Preserve(AllMembers = true)]
public static partial class FluentIconsFilled
{
    static FluentIconsFilled() { }

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
