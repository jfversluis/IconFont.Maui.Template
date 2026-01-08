using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace IconFont.Maui.Template.IntegrationTests;

public class BuildTests
{
    [Fact]
    public void Generates_Config_Files_On_Build()
    {
        if (!IsMac()) return;

        var repoRoot = GetRepoRoot();
        var proj = Path.Combine(repoRoot, "src", "IconFont.Maui.Template", "IconFont.Maui.Template.csproj");
        var psi = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "build \"" + proj + "\" -f net10.0-maccatalyst -v minimal",
            WorkingDirectory = repoRoot,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        var proc = Process.Start(psi)!;
        var output = proc.StandardOutput.ReadToEnd();
        var err = proc.StandardError.ReadToEnd();
        proc.WaitForExit();
        Assert.True(proc.ExitCode == 0, $"build failed: {output}\n{err}");

        var objRoot = Path.Combine(repoRoot, "src", "IconFont.Maui.Template", "obj");
        var config = Directory.EnumerateFiles(objRoot, "IconFontConfig.g.cs", SearchOption.AllDirectories).FirstOrDefault();
        var extensions = Directory.EnumerateFiles(objRoot, "IconFontExtensions.g.cs", SearchOption.AllDirectories).FirstOrDefault();
        Assert.True(config != null, "IconFontConfig.g.cs not found");
        Assert.True(extensions != null, "IconFontExtensions.g.cs not found");
    }

    private static bool IsMac() => OperatingSystem.IsMacOS();

    private static string GetRepoRoot()
    {
        var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "IconFont.Maui.Template.sln")))
            dir = dir.Parent!;
        return dir?.FullName ?? Directory.GetCurrentDirectory();
    }
}
