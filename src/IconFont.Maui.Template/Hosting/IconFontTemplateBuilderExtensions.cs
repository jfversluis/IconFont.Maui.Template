using Microsoft.Maui.Hosting;

namespace IconFontTemplate;

public static partial class IconFontTemplateBuilderExtensions
{
    /// <summary>
    /// Registers the Fluent icon font alias. Useful for project references if you want explicit initialization.
    /// </summary>
    public static MauiAppBuilder UseIconFont(this MauiAppBuilder builder)
    {
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont(IconFontConfig.FontFile, IconFontConfig.FontAlias);
        });
        return builder;
    }
}
