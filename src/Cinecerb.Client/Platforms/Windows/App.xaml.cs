using Microsoft.AspNetCore.Components.WebView.Maui;

namespace Cinecerb.Client.Platforms.Windows;

public partial class App : MauiWinUIApplication
{
    public App() => InitializeComponent();

    protected override MauiApp CreateMauiApp() =>
        MauiProgram.CreateMauiApp(ConfigureServices);

    private static void ConfigureServices(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IWindowCreator, CinecerbWindowCreator>();
        builder.ConfigureMauiHandlers(handlers =>
            handlers.AddHandler<BlazorWebView, CinecerbBlazorWebViewHandler>());
    }
}
