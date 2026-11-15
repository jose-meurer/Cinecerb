namespace Cinecerb.Client.Shared.DesignSystem;

internal static class AppTheme
{
    public static Color PrimaryColor => GetColor(nameof(PrimaryColor));
    public static Color PrimaryHoverColor => GetColor(nameof(PrimaryHoverColor));
    public static Color PrimaryTintColor => GetColor(nameof(PrimaryTintColor));
    public static Color SecondaryColor => GetColor(nameof(SecondaryColor));
    public static Color SecondaryHoverColor => GetColor(nameof(SecondaryHoverColor));

    public static Color GlowSoftColor => GetColor(nameof(GlowSoftColor));
    public static Color GlowColor => GetColor(nameof(GlowColor));
    public static Color GlowStrongColor => GetColor(nameof(GlowStrongColor));
    public static Color GlowIntenseColor => GetColor(nameof(GlowIntenseColor));

    public static Color BackgroundColor => GetColor(nameof(BackgroundColor));
    public static Color SurfaceColor => GetColor(nameof(SurfaceColor));
    public static Color SurfaceRaisedColor => GetColor(nameof(SurfaceRaisedColor));
    public static Color SurfaceOverlayColor => GetColor(nameof(SurfaceOverlayColor));
    public static Color SurfaceInputColor => GetColor(nameof(SurfaceInputColor));
    public static Color SurfaceInputHoverColor => GetColor(nameof(SurfaceInputHoverColor));
    public static Color SurfaceInputFocusColor => GetColor(nameof(SurfaceInputFocusColor));
    public static Color TitleBarBackgroundColor => GetColor(nameof(TitleBarBackgroundColor));

    public static Color BorderColor => GetColor(nameof(BorderColor));
    public static Color BorderPrimaryColor => GetColor(nameof(BorderPrimaryColor));
    public static Color BorderPrimaryStrongColor => GetColor(nameof(BorderPrimaryStrongColor));
    public static Color FocusRingColor => GetColor(nameof(FocusRingColor));

    public static Color HighlightColor => GetColor(nameof(HighlightColor));
    public static Color HighlightSubtleColor => GetColor(nameof(HighlightSubtleColor));
    public static Color ShadowColor => GetColor(nameof(ShadowColor));
    public static Color ShadowSubtleColor => GetColor(nameof(ShadowSubtleColor));
    public static Color ShadowStrongColor => GetColor(nameof(ShadowStrongColor));
    public static Color ShadowInsetColor => GetColor(nameof(ShadowInsetColor));
    public static Color AmbientShadowColor => GetColor(nameof(AmbientShadowColor));
    public static Color TextShadowColor => GetColor(nameof(TextShadowColor));

    public static Color TextPrimaryColor => GetColor(nameof(TextPrimaryColor));
    public static Color TextSecondaryColor => GetColor(nameof(TextSecondaryColor));
    public static Color TextPlaceholderColor => GetColor(nameof(TextPlaceholderColor));
    public static Color IconColor => GetColor(nameof(IconColor));

    public static Color TrackSubtleColor => GetColor(nameof(TrackSubtleColor));
    public static Color TrackColor => GetColor(nameof(TrackColor));

    public static Color AssetForegroundColor => GetColor(nameof(AssetForegroundColor));

    private static Color GetColor(string key)
    {
        if (Application.Current?.Resources.TryGetValue(key, out var resource) == true && resource is Color color)
            return color;

        throw new InvalidOperationException($"A cor '{key}' não está definida em AppColors.xaml.");
    }
}
