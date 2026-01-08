using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.RegularExpressions;
using IconFontTemplate;

namespace IconFontTemplate.Sample.ViewModels;

public class IconGlyph
{
    public required string Glyph { get; init; }
    // e.g., FluentIcons.Regular.Add24
    public required string Identifier { get; init; }
    // e.g., icons:FluentIcons.Regular.Add24
    public required string XamlIdentifier { get; init; }
    public required string FontFamily { get; init; }
}

public class IconsViewModel
{
    public ObservableCollection<IconGlyph> Icons { get; } = new();

    public IconsViewModel()
    {
        AddIcons(typeof(FluentIcons.Regular), FluentIcons.FontFamily, "FluentIcons.Regular");
		if (Type.GetType("IconFontTemplate.FluentIconsFilled+Filled, IconFont.Maui.Template") is Type filledType)
		{
			AddIcons(filledType, FluentIconsFilled.FontFamily, "FluentIconsFilled.Filled");
		}
    }

    private void AddIcons(Type type, string fontFamily, string identifierPrefix)
    {
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in fields)
        {
            if (field.FieldType != typeof(string)) continue;
            var glyph = field.GetValue(null) as string;
            if (string.IsNullOrEmpty(glyph)) continue;
            Icons.Add(new IconGlyph
            {
                Glyph = glyph!,
                FontFamily = fontFamily,
                Identifier = $"{identifierPrefix}.{field.Name}",
                XamlIdentifier = $"icons:{identifierPrefix}.{field.Name}"
            });
        }
    }
}
