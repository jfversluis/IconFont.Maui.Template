<!--
Everything in here is optional—adapt as needed.
Replace placeholders before publishing:
- {{PACKAGE_ID}}           → e.g., IconFont.FluentUI
- {{FONT_ALIAS}}           → e.g., FluentIcons
- {{FONT_CLASS}}           → e.g., FluentIcons
- {{FONT_NAMESPACE}}       → e.g., IconFont.FluentUI
- {{FONT_LICENSE}}         → e.g., MIT (upstream font license)
- {{FONT_LICENSE_LINK}}    → e.g., https://github.com/microsoft/fluentui-system-icons/blob/main/LICENSE
- {{FONT_FILE}}            → e.g., FluentSystemIcons-Regular.ttf

NuGet.org allows only images from certain domains. Use raw.githubusercontent.com for badges/images.
Example badge:
[![NuGet](https://img.shields.io/nuget/v/{{PACKAGE_ID}}.svg?label=NuGet)](https://www.nuget.org/packages/{{PACKAGE_ID}})
--
Recommended for maintainers of this template:
- Rename this file to `README.md` in your fork.
- Update your `.csproj`:
    <PackageReadmeFile>README.md</PackageReadmeFile>
    <None Include="..\\..\\README.md" Pack="true" PackagePath="" />
-->

[![NuGet](https://img.shields.io/nuget/v/{{PACKAGE_ID}}.svg?label=NuGet)](https://www.nuget.org/packages/{{PACKAGE_ID}})

# {{PACKAGE_ID}}

`{{PACKAGE_ID}}` ships the `{{FONT_FILE}}` icon font for .NET MAUI. It registers the font across supported targets when you call `Use{{FONT_CLASS}}()` and exposes strongly-typed glyph constants via `{{FONT_CLASS}}` to simplify XAML and C# usage.

## ✨ Features
- ⚙️ **One-line setup**: call `builder.Use{{FONT_CLASS}}()` (generated, e.g., `UseFluentIcons()`) in `MauiProgram`
- ➕ **Multiple fonts**: use `builder.Use{{FONT_CLASS}}()` to register all, or per-font helpers like `Use{{FONT_CLASS}}Filled()`
- 🔤 **Strongly-typed glyphs** via `{{FONT_CLASS}}Regular.*`, `{{FONT_CLASS}}Filled.*` (and other styles if present)
- 🧰 **Helper APIs**: `{{FONT_CLASS}}.Create()` for `FontImageSource`
- 📱 **Supported targets**: Android, iOS, Mac Catalyst, Windows

## 📦 Install
```bash
dotnet add package {{PACKAGE_ID}}
```

## 🚀 Getting Started

### Register
```csharp
var builder = MauiApp.CreateBuilder()
    .UseMauiApp<App>()
    .Use{{FONT_CLASS}}(); // e.g., UseFluentIcons()
```

### XAML usage
```xaml
xmlns:icons="clr-namespace:{{FONT_NAMESPACE}};assembly={{PACKAGE_ID}}"

<FontImageSource Glyph="{x:Static icons:{{FONT_CLASS}}Regular.Add24}"
                 FontFamily="{x:Static icons:{{FONT_CLASS}}.FontFamily}"
                 Color="#2563EB"
                 Size="32" />
```

### C# usage
```csharp
using {{FONT_NAMESPACE}};

// Create a FontImageSource for any glyph
var source = {{FONT_CLASS}}.Create({{FONT_CLASS}}Regular.Add24, Colors.Orange, 32);
```

> **Tip:** Glyph names follow the upstream font. If the font adds/changes glyphs, updating the font file (TTF or OTF) and rebuilding regenerates this API.

## 📋 Styles & glyphs
The generator emits one top-level class per style, with the style name appended to the configured class name (e.g., `{{FONT_CLASS}}Regular`, `{{FONT_CLASS}}Filled`). This flat structure allows direct use in XAML via `{x:Static}`. Example members:

- `{{FONT_CLASS}}Regular.Add24`
- `{{FONT_CLASS}}Filled.Home24`

## 🧩 Platforms
| Platform | Minimum |
|----------|---------|
| Android  | 21+     |
| iOS      | 15+     |
| macOS    | 12+     |
| Windows  | 10 1809 |

## 📄 License
- **Library:** MIT (or your license)
- **Font:** {{FONT_LICENSE}} (confirm redistribution rights; see [license]({{FONT_LICENSE_LINK}}))

## 🙏 Attribution
- Upstream font: {{FONT_LICENSE}} © respective owners
- This project is not affiliated with or endorsed by upstream vendors.
