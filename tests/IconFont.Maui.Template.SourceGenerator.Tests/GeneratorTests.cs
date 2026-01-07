using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.Diagnostics;
using IconFontTemplate.SourceGenerator;
using Xunit;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace IconFont.Maui.Template.SourceGenerator.Tests;

public class GeneratorTests
{
    [Fact]
    public void Generates_FluentIcons_With_Defaults()
    {
        var fontPath = Path.GetFullPath(Path.Combine(GetRepoRoot(), "src/IconFont.Maui.Template/Resources/Fonts/FluentSystemIcons-Regular.ttf"));
        Assert.True(File.Exists(fontPath));

        var compilation = CSharpCompilation.Create(
            "Tests",
            new[] { CSharpSyntaxTree.ParseText("""namespace Dummy { class C { } }""") },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var generator = new FluentGlyphGenerator();
        var driver = CSharpGeneratorDriver.Create(new ISourceGenerator[] { generator },
            additionalTexts: new[] { new TestAdditionalText(fontPath) },
            optionsProvider: TestAnalyzerConfigOptionsProvider.With(
                global: new()
                {
                    ["build_metadata.AdditionalFiles.IconFontFile"] = Path.GetFileName(fontPath),
                    ["build_metadata.AdditionalFiles.IconFontAlias"] = "FluentIcons",
                    ["build_metadata.AdditionalFiles.IconFontClass"] = "FluentIcons",
                    ["build_metadata.AdditionalFiles.IconFontNamespace"] = "IconFontTemplate",
                }));

        driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);
        Assert.Empty(diagnostics);

        var generated = outputCompilation.SyntaxTrees.FirstOrDefault(t => t.FilePath.EndsWith("FluentIcons.Generated.g.cs"));
        Assert.NotNull(generated);
        var text = generated!.ToString();
        Assert.Contains("namespace IconFontTemplate;", text);
        Assert.Contains("public static partial class FluentIcons", text);
        Assert.Contains("public const string Add24", text);
    }

    private static string GetRepoRoot()
    {
        var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "IconFont.Maui.Template.sln")))
            dir = dir.Parent!;
        return dir?.FullName ?? Directory.GetCurrentDirectory();
    }

    private sealed class TestAdditionalText : AdditionalText
    {
        private readonly string _path;
        public TestAdditionalText(string path) => _path = path;
        public override string Path => _path;
        public override SourceText? GetText(CancellationToken cancellationToken = default) => SourceText.From(File.ReadAllText(_path), Encoding.UTF8);
    }

    private sealed class TestAnalyzerConfigOptionsProvider : AnalyzerConfigOptionsProvider
    {
        private readonly AnalyzerConfigOptions _global;
        private TestAnalyzerConfigOptionsProvider(AnalyzerConfigOptions global) => _global = global;
        public static AnalyzerConfigOptionsProvider With(Dictionary<string, string> global)
            => new TestAnalyzerConfigOptionsProvider(new TestAnalyzerConfigOptions(global));
        public override AnalyzerConfigOptions GlobalOptions => _global;
        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => TestAnalyzerConfigOptions.Empty;
        public override AnalyzerConfigOptions GetOptions(AdditionalText text) => TestAnalyzerConfigOptions.Empty;
    }

    private sealed class TestAnalyzerConfigOptions : AnalyzerConfigOptions
    {
        private readonly IReadOnlyDictionary<string, string> _dict;
        public static readonly AnalyzerConfigOptions Empty = new TestAnalyzerConfigOptions(new Dictionary<string, string>());
        public TestAnalyzerConfigOptions(IReadOnlyDictionary<string, string> dict) => _dict = dict;
        public override bool TryGetValue(string key, out string value) => _dict.TryGetValue(key, out value!);
    }
}
