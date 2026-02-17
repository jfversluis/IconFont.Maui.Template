using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.RegularExpressions;
using IconFontTemplate;

namespace IconFontTemplate.Sample.ViewModels;

public class IconGlyph
{
    public required string Glyph { get; init; }
    // e.g., FluentIconsRegular.Add24
    public required string Identifier { get; init; }
    // e.g., icons:FluentIconsRegular.Add24
    public required string XamlIdentifier { get; init; }
    public required string FontFamily { get; init; }
}

public class IconsViewModel
{
    public ObservableCollection<IconGlyph> Icons { get; } = new();

    public IconsViewModel(string? fontClass = null)
    {
        var asm = typeof(FluentIcons).Assembly;
        foreach (var cfg in IconFontConfigs.All)
        {
            if (fontClass is not null && !string.Equals(cfg.ClassName, fontClass, StringComparison.Ordinal))
                continue;

            // Find all flat classes whose name starts with the configured class name
            // e.g., "FluentIcons" matches "FluentIconsRegular", "FluentIconsFilled", etc.
            var matchingTypes = asm.GetTypes()
                .Where(t => t.IsAbstract && t.IsSealed // static class
                    && t.Namespace == cfg.Namespace
                    && t.Name.StartsWith(cfg.ClassName, StringComparison.Ordinal)
                    && t.Name != cfg.ClassName) // exclude the helper class itself
                .OrderBy(t => t.Name, StringComparer.Ordinal);

            foreach (var type in matchingTypes)
            {
                AddIcons(type, cfg.FontAlias, type.Name);
            }
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
