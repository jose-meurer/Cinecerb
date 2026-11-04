using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;
using AppTheme = Cinecerb.Client.Shared.DesignSystem.AppTheme;

namespace Cinecerb.Client.Platforms.Windows;

internal sealed class CinecerbBlazorWebViewHandler : BlazorWebViewHandler
{
    protected override WebView2 CreatePlatformView()
    {
        var background = AppTheme.Background;
        Environment.SetEnvironmentVariable("WEBVIEW2_DEFAULT_BACKGROUND_COLOR", background.ToArgbHex().TrimStart('#'));

        var webView = base.CreatePlatformView();
        webView.DefaultBackgroundColor = background.ToWindowsColor();
        return webView;
    }
}
