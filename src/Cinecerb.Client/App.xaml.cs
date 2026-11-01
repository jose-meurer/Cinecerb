using Microsoft.Maui.ApplicationModel;

namespace Cinecerb.Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new MainPage()) { Title = AppInfo.Current.Name };
        ConfigurePlatformWindow(window);

        return window;
    }

    partial void ConfigurePlatformWindow(Window window);
}
