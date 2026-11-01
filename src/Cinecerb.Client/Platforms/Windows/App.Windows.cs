using Cinecerb.Client.WinUI;

namespace Cinecerb.Client;

public partial class App
{
    partial void ConfigurePlatformWindow(Window window)
    {
        window.TitleBar = new CinecerbTitleBar();
        window.Created += SetWindowIcon;
    }

    private static void SetWindowIcon(object? sender, EventArgs args)
    {
        if (sender is not Window window ||
            window.Handler?.PlatformView is not Microsoft.Maui.MauiWinUIWindow nativeWindow)
        {
            return;
        }

        var iconPath = Path.Combine(AppContext.BaseDirectory, "Assets", "app.ico");
        nativeWindow.AppWindow.SetIcon(iconPath);
    }
}
