namespace TowerOfBabel;
public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();
        Title=App.d["Informazioni"] as string;
    }
    private async void OnInformazioni_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(new Uri("https://github.com/numerunix/solitario.maui"));
    }
}