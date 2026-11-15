using Microsoft.Extensions.Logging;
using Cinecerb.Client.Composition;
namespace Cinecerb.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp(Action<MauiAppBuilder> configurePlatform)
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddCinecerbServices();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        configurePlatform(builder);

        return builder.Build();
    }
}
