namespace MyPlanner;

public partial class SettingsPage : ContentPage
{
	public string Color { get; set; }

    public int FontSize {  get; set; } = 16;

    public event EventHandler<bool> fontChanged;

	public SettingsPage()
	{
		InitializeComponent();
		BindingContext = this;
        WhiteRadioButton.IsChecked = true;
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
        await Navigation.PopModalAsync();
    }

    private async void SaveStyle_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void TextSizeStepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        FontSize = (int)e.NewValue;
        fontChanged?.Invoke(sender, true);
    }

    
}