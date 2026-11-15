using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using AppTheme = Cinecerb.Client.Shared.DesignSystem.AppTheme;

namespace Cinecerb.Client.Platforms.Windows;

internal sealed class CinecerbBlazorWebViewHandler : BlazorWebViewHandler
{
    protected override WebView2 CreatePlatformView()
    {
        var webView = base.CreatePlatformView();
        webView.DefaultBackgroundColor = AppTheme.BackgroundColor.ToWindowsColor();
        webView.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        webView.NavigationCompleted += ShowWhenLoaded;
        return webView;
    }

    private static void ShowWhenLoaded(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
    {
        sender.NavigationCompleted -= ShowWhenLoaded;
        sender.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
    }
}
