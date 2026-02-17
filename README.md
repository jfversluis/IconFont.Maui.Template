# IconFont.Maui.Template for .NET MAUI

IconFont.Maui.Template makes it painless to consume the [Fluent UI System Icons](https://github.com/microsoft/fluentui-system-icons) font inside .NET MAUI applications. It bundles the official font file, registers it at build time using a `buildTransitive` target, and exposes helper APIs plus a sample app that demonstrates how to render glyphs in XAML and C#.

ℹ️ **Customize quickly:** drop your TTF into `src/IconFont.Maui.Template/Resources/Fonts/` and edit `src/IconFont.Maui.Template/IconFont.props` (file, alias, class, namespace). Everything else updates automatically.

## 📦 Architecture

This repo contains **two packages**:

| Package | Purpose |
|---------|---------|
| **`IconFont.Maui.SourceGenerator`** | Shared Roslyn source generator + MSBuild targets. Parses any TTF and emits strongly-typed glyph constants. Published once, referenced by all font packages. |
| **`IconFont.Maui.Template`** | Template library that bundles a specific font (Fluent UI icons by default). Clone this, drop in your font, and publish your own package. |

When you clone this template for your own font, replace the `ProjectReference` to the source generator with a `PackageReference` to `IconFont.Maui.SourceGenerator` — see instructions in the `.csproj`.

## ✨ Features

- 📦 **Drop-in NuGet packaging** – The `IconFont.Maui.Template` library automatically registers the configured font alias for every target (Android, iOS, Mac Catalyst, Windows).
- 🧱 **Helper APIs** – Use the `FluentIcons` helper class to reference glyphs and create `FontImageSource` instances in code.
- ⚙️ **Shared source generator** – `IconFont.Maui.SourceGenerator` parses any TTF and emits strongly-typed glyph constants. Published as a separate NuGet so all font packages share the same generator.
- 🧪 **Sample MAUI app** – `IconFont.Maui.Template.Sample` shows how to consume the library and render icons in XAML without manual font setup.
- 📄 **MIT licensed** – The library is MIT licensed and redistributes the Fluent UI System Icons font under its MIT license.

## 🚀 Getting Started

1. **Install the package** (when published):
   ```bash
   dotnet add package IconFont.Maui.Template
   ```
2. **Use the `FluentIconsRegular` class** in XAML:
   ```xaml
   xmlns:icons="clr-namespace:IconFontTemplate;assembly=IconFont.Maui.Template"
   ...
   <FontImageSource Glyph="{x:Static icons:FluentIconsRegular.Add24}"
                    FontFamily="{x:Static icons:FluentIcons.FontFamily}"
                    Color="#2563EB"
                    Size="48" />
   ```
3. **Or in C#:**
   ```csharp
   using IconFontTemplate;

   var imageSource = FluentIcons.Create(FluentIconsRegular.Calendar24, Colors.Orange, 32);
   ```

The font is registered automatically via `buildTransitive/IconFont.Maui.Template.targets`, so no changes to your app's `.csproj` are required. For project references or explicit registration, call `builder.UseIconFont()`.

### Customize glyph coverage

Out of the box, every glyph encoded in the Fluent TTF’s Private Use Area is exposed via `FluentIcons{Style}.GlyphName` (e.g. `FluentIconsRegular.Add24`. If you need to filter or rename generated output, adjust `FluentGlyphGenerator` to apply your own grouping rules (for example, to emit only a subset or inject friendly descriptions).

> Tip: When Fluent UI ships new icons, update `FluentSystemIcons-Regular.ttf` (or your configured font), drop it into `Resources/Fonts`, and rebuild—the generator will pick up the new glyphs automatically with no extra metadata files.

## 🛠 Customize for your font

1. Clone/fork this template repo.
2. Drop your font into `src/IconFont.Maui.Template/Resources/Fonts/` (e.g., `MyFont.ttf`).
3. Edit `src/IconFont.Maui.Template/IconFont.props`:
   - `IconFontFile` → `MyFont.ttf`
   - `IconFontAlias` → `MyFont`
   - *(optional)* `IconFontClass`, `IconFontNamespace`
4. In `IconFont.Maui.Template.csproj`, replace the `ProjectReference` with:
   ```xml
   <PackageReference Include="IconFont.Maui.SourceGenerator" Version="1.0.0"
       OutputItemType="Analyzer" ReferenceOutputAssembly="false" PrivateAssets="all" />
   ```
   and remove the `<Import>` of the generator targets (it ships automatically with the package).
5. Build: `dotnet build IconFont.Maui.Template.sln`
6. Sample app:
   ```csharp
   builder.UseMauiApp<App>()
          .UseIconFont();
   ```
7. XAML usage adapts automatically if you keep defaults; otherwise update `xmlns` and class tokens.

### Multi-font example
```xml
<ItemGroup>
   <IconFontDefinition Include="Resources/Fonts/MyIcons-Regular.ttf">
      <FontAlias>MyIcons</FontAlias>
      <FontClass>MyIcons</FontClass>
      <FontNamespace>MyCompany.Icons</FontNamespace>
   </IconFontDefinition>
   <IconFontDefinition Include="Resources/Fonts/MyIcons-Filled.ttf">
      <FontAlias>MyIconsFilled</FontAlias>
      <FontClass>MyIconsFilled</FontClass>
      <FontNamespace>MyCompany.Icons</FontNamespace>
   </IconFontDefinition>
</ItemGroup>
```
In `MauiProgram`:
```csharp
builder.UseIconFonts(); // registers all fonts
// or builder.UseMyIcons(); builder.UseMyIconsFilled();
```

🔒 **Licensing checklist (for authors):**
- **Verify** your font’s license permits **redistribution** (NuGet/package) and use in apps.
- **Update** `NOTICE.md` with font **name**, **author**, **source URL**, and **license text**.
- **Align** `README_Consumer.md` placeholders (font license/link/attribution) with your font.
- If the font license isn’t MIT, keep code under MIT but include the font license via `NOTICE.md` (packed) or `PackageLicenseFile`.

## 🧑‍💻 Consumer README template

- Use `README_Consumer.md` as a starting point for your NuGet-facing README. Replace placeholders (package ID, namespace, class, font file, license) before publishing.
- **Recommended:** rename `README_Consumer.md` to `README.md` in your fork and update your `.csproj`:
   ```xml
   <PackageReadmeFile>README.md</PackageReadmeFile>
   <None Include="..\\..\\README.md" Pack="true" PackagePath="" />
   ```
- Alternatively, keep `README_Consumer.md` and set in your `.csproj`:
   ```xml
   <PackageReadmeFile>README_Consumer.md</PackageReadmeFile>
   ```
   Ensure it’s packed:
   ```xml
   <None Include="README_Consumer.md" Pack="true" PackagePath="" />
   ```


## 📦 Publishing

- This template is ready for **NuGet Trusted Publishing** (OIDC-based, no API keys).
- The release workflow packs **both** `IconFont.Maui.SourceGenerator` and `IconFont.Maui.Template`.
- GitHub Actions release workflow uses `nuget/setup-nuget@v1` with `auth: true` and `id-token` permissions.
- See: https://blog.verslu.is/nuget/trusted-publishing-easy-setup/

## 🏗️ Repository Layout

```
IconFont/
├── IconFont.Maui.Template.sln
├── .github/
│   ├── copilot-instructions.md
│   └── workflows/
│       ├── ci.yml
│       └── release.yml
├── src/
│   ├── IconFont.Maui.Template/              ← Template font library (clone & customize)
│   │   ├── IconFont.props                   ← Font configuration (file, alias, class, namespace)
│   │   ├── IconFont.Maui.Template.csproj
│   │   ├── FluentIcons.cs                   ← Helper class (Create, FontFamily)
│   │   ├── FluentIconsFilled.cs
│   │   ├── FluentIconsInitializer.cs
│   │   ├── Hosting/IconFontBuilderExtensions.cs
│   │   ├── Resources/Fonts/*.ttf
│   │   └── buildTransitive/IconFont.Maui.Template.targets
│   └── IconFont.Maui.Template.SourceGenerator/  ← Shared generator (published as NuGet)
│       ├── IconFont.Maui.Template.SourceGenerator.csproj
│       ├── FluentGlyphGenerator.cs
│       └── buildTransitive/IconFont.Maui.SourceGenerator.targets
├── tests/
│   ├── IconFont.Maui.Template.SourceGenerator.Tests/
│   └── IconFont.Maui.Template.IntegrationTests/
└── samples/
    └── IconFont.Maui.Template.Sample/
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
