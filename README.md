# IconFont.Maui.Template for .NET MAUI

IconFont.Maui.Template makes it painless to consume the [Fluent UI System Icons](https://github.com/microsoft/fluentui-system-icons) font inside .NET MAUI applications. It bundles the official font file, registers it at build time using a `buildTransitive` target, and exposes helper APIs plus a sample app that demonstrates how to render glyphs in XAML and C#.

ℹ️ **Customize quickly:** drop your TTF into `src/IconFont.Maui.Template/Resources/Fonts/` and edit `src/IconFont.Maui.Template/IconFont.props` (file, alias, class, namespace). Everything else updates automatically.

## ✨ Features

- 📦 **Drop-in NuGet packaging** – The `IconFont.Maui.Template` library automatically registers the configured font alias for every target (Android, iOS, Mac Catalyst, Windows).
- 🧱 **Helper APIs** – Use the `FluentIcons` helper class to reference glyphs and create `FontImageSource` instances in code.
- ⚙️ **Build-time glyph generator** – `IconFont.Maui.Template.SourceGenerator` parses the configured TTF and emits strongly-typed glyph constants during compilation.
- 🧪 **Sample MAUI app** – `IconFont.Maui.Template.Sample` shows how to consume the library and render icons in XAML without manual font setup.
- 📄 **MIT licensed** – The library is MIT licensed and redistributes the Fluent UI System Icons font under its MIT license.

## 🚀 Getting Started

1. **Install the package** (when published):
   ```bash
   dotnet add package IconFont.Maui.Template
   ```
2. **Use the `FluentIcons` alias** in XAML:
   ```xaml
   xmlns:icons="clr-namespace:IconFontTemplate;assembly=IconFont.Maui.Template"
   ...
   <FontImageSource Glyph="{x:Static icons:FluentIcons.Regular.Add24}"
                    FontFamily="{x:Static icons:FluentIcons.FontFamily}"
                    Color="#2563EB"
                    Size="48" />
   ```
3. **Or in C#:**
   ```csharp
   using IconFontTemplate;

   var imageSource = FluentIcons.Create(FluentIcons.Regular.Calendar24, Colors.Orange, 32);
   ```

The font is registered automatically via `buildTransitive/IconFont.Maui.Template.targets`, so no changes to your app's `.csproj` are required. For project references or explicit registration, call `builder.UseIconFont()`.

### Customize glyph coverage

Out of the box, every glyph encoded in the Fluent TTF’s Private Use Area is exposed via `FluentIcons.<Style>.GlyphName`. If you need to filter or rename generated output, adjust `FluentGlyphGenerator` to apply your own grouping rules (for example, to emit only a subset or inject friendly descriptions).

> Tip: When Fluent UI ships new icons, update `FluentSystemIcons-Regular.ttf` (or your configured font), drop it into `Resources/Fonts`, and rebuild—the generator will pick up the new glyphs automatically with no extra metadata files.

## 🛠 Customize for your font

1. Drop your font into `src/IconFont.Maui.Template/Resources/Fonts/` (e.g., `MyFont.ttf`).
2. Edit `src/IconFont.Maui.Template/IconFont.props`:
   - `IconFontFile` → `MyFont.ttf`
   - `IconFontAlias` → `MyFont`
   - *(optional)* `IconFontClass`, `IconFontNamespace`
3. Build: `dotnet build IconFont.Maui.Template.sln`
4. Sample app:
   ```csharp
   builder.UseMauiApp<App>()
          .UseIconFont();
   ```
5. XAML usage adapts automatically if you keep defaults; otherwise update `xmlns` and class tokens.

## 📦 Publishing

- This template is ready for **NuGet Trusted Publishing** (OIDC-based, no API keys).
- GitHub Actions release workflow uses `nuget/setup-nuget@v1` with `auth: true` and `id-token` permissions.
- See: https://blog.verslu.is/nuget/trusted-publishing-easy-setup/

## 🏗️ Repository Layout

```
IconFont/
├── IconFont.Maui.Template.sln
├── .github/
│   └── copilot-instructions.md
├── src/
│   ├── IconFont.Maui.Template/
|   │   ├── IconFont.props
|   │   ├── IconFont.Maui.Template.csproj
|   │   ├── FluentIcons.cs
|   │   ├── Hosting/IconFontTemplateBuilderExtensions.cs
|   │   ├── FluentIconsInitializer.cs
|   │   ├── Resources/Fonts/FluentSystemIcons-Regular.ttf
|   │   └── buildTransitive/IconFont.Maui.Template.targets
│   └── IconFont.Maui.Template.SourceGenerator/
│       ├── IconFont.Maui.Template.SourceGenerator.csproj
│       └── FluentGlyphGenerator.cs
└── samples/
      └── IconFont.Maui.Template.Sample/
         ├── IconFont.Maui.Template.Sample.csproj
        └── MainPage.xaml (+ code-behind)
```

## 🧪 Building & Testing

```bash
# Restore and build all targets
cd IconFont
 dotnet build IconFont.Maui.Template.sln

# Run the sample app (choose a platform)
dotnet build samples/IconFont.Maui.Template.Sample/IconFont.Maui.Template.Sample.csproj -t:Run -f net10.0-ios -r iossimulator-arm64
dotnet build samples/IconFont.Maui.Template.Sample/IconFont.Maui.Template.Sample.csproj -t:Run -f net10.0-maccatalyst
dotnet build samples/IconFont.Maui.Template.Sample/IconFont.Maui.Template.Sample.csproj -t:Run -f net10.0-android
```

> **Note:** The sample uses the regular Fluent font. If you ship filled/other styles, include additional `.targets` files and alias names.

## 📚 Licensing

- **Library**: MIT License (see [`LICENSE`](LICENSE)).
- **Fluent UI System Icons font**: MIT License © Microsoft Corporation. See [`NOTICE.md`](NOTICE.md) for attribution and upstream license text.
- This project is not affiliated with or endorsed by Microsoft. Trademarks belong to their respective owners.

## 🙌 Contributing

Pull requests are welcome! Please ensure any new icons are sourced from `main` in the Fluent UI repository and that licensing headers remain intact.
