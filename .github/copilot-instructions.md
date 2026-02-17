# IconFont.Maui Template — Copilot Instructions

This is a **template repository** for creating .NET MAUI icon font NuGet packages. When a user clones this repo to create their own icon font package, they will need to rename everything from the template defaults to match their icon font.

## Renaming the Template

When a user asks to rename/customize this template for their icon font (e.g., "BootstrapIcons", "MaterialIcons", etc.), perform **all** of the following steps. The user will provide:
- **Font name**: e.g., `BootstrapIcons` (used for class names, aliases, NuGet package ID)
- **TTF file(s)**: the icon font file(s) to use

### Placeholders to replace

| Placeholder | Example replacement | Where |
|---|---|---|
| `IconFont.Maui.Template` | `IconFont.Maui.BootstrapIcons` | Solution file, csproj filenames, project paths, NuGet PackageId, namespaces in CI/CD workflows |
| `IconFontTemplate` (namespace) | `IconFont.Maui.BootstrapIcons` | All `.cs`, `.xaml`, `.csproj`, `IconFont.props` namespace references |
| `FluentIcons` / `FluentIconsFilled` | `BootstrapIcons` | Class names, FontAlias, FontClass in `IconFont.props`, helper `.cs` files, sample app references |
| `FluentSystemIcons-Regular.ttf` / `FluentSystemIcons-Filled.ttf` | `bootstrap-icons.ttf` | `IconFont.props`, `Resources/Fonts/` directory, `buildTransitive/` targets |

### Files that need content changes (source files only, ignore bin/obj)

1. **`IconFont.props`** — Update `IconFontDefinition` items: font file paths, `FontAlias`, `FontClass`, `FontNamespace`
2. **`src/IconFont.Maui.Template/IconFont.Maui.Template.csproj`** — PackageId, Title, Description, RepositoryUrl, PackageTags
3. **`src/IconFont.Maui.Template/FluentIcons.cs`** — Rename class, update namespace, update doc comments
4. **`src/IconFont.Maui.Template/FluentIconsFilled.cs`** — Rename or remove (if single-style font)
5. **`src/IconFont.Maui.Template/FluentIconsInitializer.cs`** — Rename class, update namespace
6. **`src/IconFont.Maui.Template/Hosting/IconFontTemplateBuilderExtensions.cs`** — Rename class and method names
7. **`samples/IconFont.Maui.Template.Sample/**`** — Update namespaces, `xmlns` declarations, `x:Static` references, `MauiProgram.cs` builder calls
8. **`tests/IconFont.Maui.Template.IntegrationTests/BuildTests.cs`** — Update project path references and class name assertions
9. **`IconFont.Maui.Template.sln`** — Update project names and paths
10. **`buildTransitive/IconFont.Maui.Template.targets`** — Update filename and font paths
11. **`README.md`** and **`README_Consumer.md`** — Update all references
12. **`.github/workflows/ci.yml`** and **`release.yml`** — Update project paths

### Directories and files to rename

- `src/IconFont.Maui.Template/` → `src/IconFont.Maui.{FontName}/`
- `src/IconFont.Maui.Template/IconFont.Maui.Template.csproj` → matching new folder name
- `src/IconFont.Maui.Template/FluentIcons.cs` → `{FontName}.cs`
- `src/IconFont.Maui.Template/FluentIconsFilled.cs` → `{FontName}Filled.cs` (or delete if single-style)
- `src/IconFont.Maui.Template/FluentIconsInitializer.cs` → `{FontName}Initializer.cs`
- `samples/IconFont.Maui.Template.Sample/` → `samples/IconFont.Maui.{FontName}.Sample/`
- `samples/.../IconFont.Maui.Template.Sample.csproj` → matching new folder name
- `tests/IconFont.Maui.Template.IntegrationTests/` → `tests/IconFont.Maui.{FontName}.IntegrationTests/`
- `IconFont.Maui.Template.sln` → `IconFont.Maui.{FontName}.sln`
- `buildTransitive/IconFont.Maui.Template.targets` → `buildTransitive/IconFont.Maui.{FontName}.targets`

### Font files

- Remove the sample TTFs from `Resources/Fonts/` (`FluentSystemIcons-Regular.ttf`, `FluentSystemIcons-Filled.ttf`)
- Add the user's TTF file(s) to `Resources/Fonts/`
- If the font has only one style, remove the `FluentIconsFilled.cs` helper and the second `IconFontDefinition` from `IconFont.props`

### After renaming

1. Clean build artifacts: `dotnet clean` or delete `bin/` and `obj/` folders
2. Build the solution to verify the source generator produces the expected glyph constants
3. Run the integration tests
4. Update the README with correct usage examples

## Architecture

- **`IconFont.Maui.SourceGenerator`** (NuGet reference, build-time only) — Roslyn source generator that parses TTF files and emits strongly-typed glyph constants as `const string` fields
- **`IconFont.props`** — Central configuration defining which fonts to process, with what class names and aliases
- **Helper classes** (`FluentIcons.cs`, etc.) — Hand-written `partial` classes with `[Preserve]` and convenience `Create()` methods. The source generator emits the other `partial` half with all glyph constants and `FontFamily`
- **`buildTransitive/`** — MSBuild targets packed into the NuGet so consumers get automatic `MauiFont` registration

## Key conventions

- Glyph constants must be `const string` (not `static readonly`) for `{x:Static}` XAML compatibility
- `FontFamily` on generated classes is `const string` for the same reason
- Helper classes use `partial` to merge with source-generated code
- The source generator NuGet is referenced with `PrivateAssets="all"` — it does NOT become a runtime dependency
