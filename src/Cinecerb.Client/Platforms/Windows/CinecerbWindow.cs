using global::Windows.Graphics;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;

namespace Cinecerb.Client.Platforms.Windows;

internal static class CinecerbWindow
{
    public static void Configure(Microsoft.UI.Xaml.Window window)
    {
        window.AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets", "app.ico"));
        TitleBarDraggable(window);
    }

    private static void TitleBarDraggable(Microsoft.UI.Xaml.Window window)
    {
        var appWindow = window.AppWindow;
        var nonClientInput = InputNonClientPointerSource.GetForWindowId(appWindow.Id);
        var content = (FrameworkElement)window.Content;

        content.LayoutUpdated += UpdateDragArea;
        window.Closed += (_, _) => content.LayoutUpdated -= UpdateDragArea;

        void UpdateDragArea(object? sender, object args)
        {
            var titleBar = appWindow.TitleBar;
            var width = appWindow.ClientSize.Width - titleBar.LeftInset - titleBar.RightInset;

            if (width > 0)
                nonClientInput.SetRegionRects(NonClientRegionKind.Caption, [new RectInt32(titleBar.LeftInset, 0, width, titleBar.Height)]);
        }
    }
}
