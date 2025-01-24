namespace MyPlanner;

public partial class SettingsPage : ContentPage
{
	public string Color { get; set; }

    public int FontSize {  get; set; } = 16;
    public string FontFamily { get; set; } = "Arial";

    public event EventHandler<bool> fontChanged;

	public SettingsPage()
	{
		InitializeComponent();
		BindingContext = this;
        fontChanged += SettingsPage_fontChanged;
    }

    private void SettingsPage_fontChanged(object? sender, bool e)
    {
        FontLabel.Text = FontSize.ToString();
    }

    private void OnThemeColors_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton selectedRadioButton = ((RadioButton)sender);
		Color = selectedRadioButton.Value.ToString();
    }

    private async void CancelChangesButton_Clicked(object sender, EventArgs e)
    {
        Application.Current.Resources["BackgroundColor"] = "#ADD8E6";
        Application.Current.Resources["ButtonTextColor"] = "#FFFAFA";
        Application.Current.Resources["ButtonBackgroundColor"] = "#191970";
        Application.Current.Resources["TextColor"] = "#000000";
        FontSize = 20;
        Application.Current.Resources["BiggerFontSize"] = FontSize;
        Application.Current.Resources["SmallerFontSize"] = FontSize - 4;
        Application.Current.Resources["FontFamily"] = FontFamily;
        await Navigation.PopModalAsync();
    }

    private async void SaveStyle_Clicked(object sender, EventArgs e)
    {
        if (Color == "Blue")
        {
            Application.Current.Resources["BackgroundColor"] = "#ADD8E6";
            Application.Current.Resources["ButtonTextColor"] = "#FFFAFA";
            Application.Current.Resources["ButtonBackgroundColor"] = "#191970";
            Application.Current.Resources["TextColor"] = "#000000";
        }
        if (Color == "Pink")
        {
            Application.Current.Resources["BackgroundColor"] = "#FF69B4";
            Application.Current.Resources["ButtonTextColor"] = "#800000";
            Application.Current.Resources["ButtonBackgroundColor"] = "#DC143C";
            Application.Current.Resources["TextColor"] = "#FFFAFA";
        }
        if(Color=="Beige")
        {
            Application.Current.Resources["BackgroundColor"] = "#FFFACD";
            Application.Current.Resources["ButtonTextColor"] = "#FFFAFA";
            Application.Current.Resources["ButtonBackgroundColor"] = "#8B4513";
            Application.Current.Resources["TextColor"] = "#000000";
        }
        if (Color == "Black")
        {
            Application.Current.Resources["BackgroundColor"] = "#000000";
            Application.Current.Resources["ButtonTextColor"] = "#FFFAFA";
            Application.Current.Resources["ButtonBackgroundColor"] = "#696969";
            Application.Current.Resources["TextColor"] = "#FFFAFA";
        }
        Application.Current.Resources["FontFamily"] = FontFamily;
        Application.Current.Resources["BiggerFontSize"] = FontSize;
        Application.Current.Resources["SmallerFontSize"] = FontSize - 4;
        await Navigation.PopModalAsync();
    }

    private void TextSizeStepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        FontSize = (int)e.NewValue;
        fontChanged?.Invoke(sender, true);
    }

    
}