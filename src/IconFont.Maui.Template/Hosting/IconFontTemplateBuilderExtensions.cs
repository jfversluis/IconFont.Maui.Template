using Microsoft.Maui.Hosting;

namespace IconFontTemplate;

public static partial class IconFontBuilderExtensions
{
    /// <summary>
    /// Registers all icon font styles defined in this package.
    /// Rename this method to match your font (e.g., UseBootstrapIcons, UseMaterialIcons).
    /// </summary>
    public static MauiAppBuilder UseFluentIcons(this MauiAppBuilder builder)
    {
        builder.ConfigureFonts(fonts =>
        {
            foreach (var cfg in IconFontConfigs.All)
            {
                fonts.AddFont(cfg.FontFile, cfg.FontAlias);
            }
        });
        return builder;
    }

    // Called by generated per-font helpers (UseFluentIconsRegular, etc.)
    internal static MauiAppBuilder UseIconFont(this MauiAppBuilder builder, string fontClass)
    {
        var cfg = System.Array.Find(IconFontConfigs.All, x => x.ClassName == fontClass);
        if (cfg is not null)
        {
            builder.ConfigureFonts(fonts => fonts.AddFont(cfg.FontFile, cfg.FontAlias));
        }
        return builder;
    }

    // Kept for backwards compatibility with generated UseIconFonts() alias
    internal static MauiAppBuilder UseIconFont(this MauiAppBuilder builder) => builder.UseFluentIcons();
}
