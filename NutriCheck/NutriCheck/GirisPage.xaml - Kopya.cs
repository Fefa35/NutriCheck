namespace NutriCheck;

public partial class GirisPage : ContentPage
{
    public GirisPage()
    {
        InitializeComponent();
    }

    private async void OnAnalizeGitClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}