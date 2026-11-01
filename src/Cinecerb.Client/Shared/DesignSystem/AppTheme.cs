namespace Cinecerb.Client.Shared.DesignSystem;

internal static class AppTheme
{
    public static Color Primary => GetColor(nameof(Primary));
    public static Color Secondary => GetColor(nameof(Secondary));
    public static Color Background => GetColor(nameof(Background));
    public static Color Surface => GetColor(nameof(Surface));
    public static Color TitleBarBackground => GetColor(nameof(TitleBarBackground));
    public static Color TextPrimary => GetColor(nameof(TextPrimary));
    public static Color BootstrapBackground => GetColor(nameof(BootstrapBackground));
    public static Color AssetForeground => GetColor(nameof(AssetForeground));

    private static Color GetColor(string key)
    {
        if (Application.Current?.Resources.TryGetValue(key, out var resource) == true && resource is Color color)
        {
            return color;
        }

        throw new InvalidOperationException($"A cor '{key}' não está definida em AppColors.xaml.");
    }
}
