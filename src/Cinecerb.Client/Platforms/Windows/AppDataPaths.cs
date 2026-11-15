namespace Cinecerb.Client.Platforms.Windows;

internal static class AppDataPaths
{
    public static string Root => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        AppInfo.Current.Name);

    public static string Database => Path.Combine(Root, "cinecerb.db");
    public static string WebView => Path.Combine(Root, "WebView2");
}
