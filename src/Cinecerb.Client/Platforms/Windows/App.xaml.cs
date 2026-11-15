using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Maui.LifecycleEvents;

namespace Cinecerb.Client.Platforms.Windows;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        InitializeComponent();
        ConfigureWebViewStorage();
    }

    protected override MauiApp CreateMauiApp() =>
        MauiProgram.CreateMauiApp(ConfigureServices);

    private static void ConfigureWebViewStorage()
    {
        Directory.CreateDirectory(AppDataPaths.WebView);
        Environment.SetEnvironmentVariable("WEBVIEW2_USER_DATA_FOLDER", AppDataPaths.WebView);
    }

    private static void ConfigureServices(MauiAppBuilder builder)
    {
        AppDatabaseStorage.Configure(builder.Services);

        builder.Services.AddSingleton<IWindowCreator, CinecerbWindowCreator>();

        builder.ConfigureLifecycleEvents(events =>
            events.AddWindows(windows => windows.OnWindowCreated(CinecerbWindow.Configure)));

        builder.ConfigureMauiHandlers(handlers =>
            handlers.AddHandler<BlazorWebView, CinecerbBlazorWebViewHandler>());
    }
}
