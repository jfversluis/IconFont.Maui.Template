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
}

public class IconsViewModel
{
    public ObservableCollection<IconGlyph> Icons { get; } = new();

    public IconsViewModel()
    {
        var fields = typeof(FluentIcons.Regular).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in fields)
        {
            if (field.FieldType == typeof(string))
            {
                var glyph = field.GetValue(null) as string;
                if (string.IsNullOrEmpty(glyph)) continue;
                Icons.Add(new IconGlyph
                {
                    Glyph = glyph!,
                    Identifier = $"FluentIcons.Regular.{field.Name}",
                    XamlIdentifier = $"icons:FluentIcons.Regular.{field.Name}"
                });
            }
        }
    }
}
