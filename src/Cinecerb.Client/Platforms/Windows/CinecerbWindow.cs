using global::Windows.Graphics;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

namespace Cinecerb.Client.Platforms.Windows;

internal static class CinecerbWindow
{
    public static void Configure(MauiWinUIWindow window)
    {
        var iconPath = Path.Combine(AppContext.BaseDirectory, "Assets", "app.ico");
        window.AppWindow.SetIcon(iconPath);

        EnableNativeDragging(window);
    }

    private static void EnableNativeDragging(MauiWinUIWindow window)
    {
        if (window.Content is not FrameworkElement root || !AppWindowTitleBar.IsCustomizationSupported())
            return;

        var appWindow = window.AppWindow;
        var input = InputNonClientPointerSource.GetForWindowId(appWindow.Id);

        void UpdateDragRegion(object? sender, object args)
        {
            var titleBar = appWindow.TitleBar;

            if (!titleBar.ExtendsContentIntoTitleBar)
                return;

            var width = appWindow.ClientSize.Width - titleBar.LeftInset - titleBar.RightInset;
            var height = Math.Min(titleBar.Height, appWindow.ClientSize.Height);

            if (width <= 0 || height <= 0)
                return;

            // A titlebar oculta do MAUI não deve manter regiões de entrada sobre os botões.
            if (input.GetRegionRects(NonClientRegionKind.Passthrough) is { Length: > 0 })
                input.ClearRegionRects(NonClientRegionKind.Passthrough);

            var rectangle = new RectInt32(titleBar.LeftInset, 0, width, height);
            var current = input.GetRegionRects(NonClientRegionKind.Caption);

            if (current is not { Length: 1 } || !current[0].Equals(rectangle))
                input.SetRegionRects(NonClientRegionKind.Caption, [rectangle]);
        }

        void OnClosed(object sender, WindowEventArgs args)
        {
            root.LayoutUpdated -= UpdateDragRegion;
            window.Closed -= OnClosed;
        }

        // Reaplica depois do layout, pois o MAUI também atualiza as regiões da titlebar.
        root.LayoutUpdated += UpdateDragRegion;
        window.Closed += OnClosed;
        UpdateDragRegion(null, EventArgs.Empty);
    }
}
