using AppTheme = Cinecerb.Client.Shared.DesignSystem.AppTheme;

namespace Cinecerb.Client.Platforms.Windows;

internal sealed class CinecerbTitleBar : TitleBar
{
    public CinecerbTitleBar()
    {
        HeightRequest = 40;
        BackgroundColor = AppTheme.TitleBarBackground;
        ForegroundColor = AppTheme.TextPrimary;
        LeadingContent = CreateBrand();
    }

    private static HorizontalStackLayout CreateBrand()
    {
        var logo = new Image
        {
            Source = "cinecerb_logo.png",
            WidthRequest = 32,
            HeightRequest = 32,
            Aspect = Aspect.AspectFit,
            VerticalOptions = LayoutOptions.Center,
            InputTransparent = true
        };

        var title = new Label
        {
            Text = AppInfo.Current.Name,
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            CharacterSpacing = 0.5,
            TextColor = AppTheme.TextPrimary,
            VerticalOptions = LayoutOptions.Center,
            InputTransparent = true
        };

        return new HorizontalStackLayout
        {
            Padding = new Thickness(16, 0),
            Spacing = 5,
            VerticalOptions = LayoutOptions.Fill,
            InputTransparent = true,
            Children = { logo, title }
        };
    }
}
