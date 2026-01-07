using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace IconFontTemplate;

public sealed class FluentIconsInitializer : IMauiInitializeService
{
    public void Initialize(IServiceProvider services)
    {
        var registrar = services.GetService<IFontRegistrar>();
        registrar?.Register(IconFontConfig.FontFile, IconFontConfig.FontAlias);
    }
}
