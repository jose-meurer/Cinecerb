using Cinecerb.Client.Composition;
using AppTheme = Cinecerb.Client.Shared.DesignSystem.AppTheme;

namespace Cinecerb.Client.Platforms.Windows;

internal sealed class CinecerbWindowCreator : IWindowCreator
{
    public Window CreateWindow(Application app, IActivationState? activationState)
    {
        var window = new MainWindow { Title = AppInfo.Current.Name };

        var titleBar = new TitleBar
        {
            HeightRequest = 40,
            BackgroundColor = AppTheme.TitleBarBackgroundColor,
            ForegroundColor = AppTheme.TextPrimaryColor
        };

        window.TitleBar = titleBar;
        titleBar.HandlerChanged += HideTitleBar;

        return window;
    }

    private static void HideTitleBar(object? sender, EventArgs args)
    {
        if (sender is not TitleBar titleBar || titleBar.Handler is null)
            return;

        titleBar.HandlerChanged -= HideTitleBar;
        titleBar.PassthroughElements.Clear();
        titleBar.IsVisible = false;
    }
}
