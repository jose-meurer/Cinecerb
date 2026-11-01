using Cinecerb.Client.Composition;

namespace Cinecerb.Client.Platforms.Windows;

internal sealed class CinecerbWindowCreator : IWindowCreator
{
    public Window CreateWindow(Application app, IActivationState? activationState)
    {
        var window = new MainWindow { Title = AppInfo.Current.Name };
        var titleBar = new CinecerbTitleBar();
        titleBar.HandlerChanged += HideSplashTitleBar;
        window.TitleBar = titleBar;
        window.Created += ConfigureNativeWindow;

        return window;
    }

    private static void HideSplashTitleBar(object? sender, EventArgs args)
    {
        if (sender is not CinecerbTitleBar titleBar || titleBar.Handler is null)
            return;

        titleBar.HandlerChanged -= HideSplashTitleBar;
        // O MAUI só remove a margem superior quando o handler nativo já existe.
        titleBar.PassthroughElements.Clear();
        titleBar.IsVisible = false;
    }

    private static void ConfigureNativeWindow(object? sender, EventArgs args)
    {
        if (sender is not Window window || window.Handler?.PlatformView is not Microsoft.Maui.MauiWinUIWindow nativeWindow)
            return;

        CinecerbWindow.Configure(nativeWindow);
    }
}
