using System;
using System.Diagnostics;
using System.IO;
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
            Arguments = "build \"" + proj + "\" -f net10.0-ios -r iossimulator-arm64 -v minimal",
            WorkingDirectory = repoRoot,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        var proc = Process.Start(psi)!;
        var output = proc.StandardOutput.ReadToEnd();
        var err = proc.StandardError.ReadToEnd();
        proc.WaitForExit();
        Assert.True(proc.ExitCode == 0, $"build failed: {output}\n{err}");

        var obj = Path.Combine(repoRoot, "src", "IconFont.Maui.Template", "obj", "Debug", "net10.0-ios");
        Assert.True(File.Exists(Path.Combine(obj, "IconFontConfig.g.cs")));
        Assert.True(File.Exists(Path.Combine(obj, "IconFontExtensions.g.cs")));
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
