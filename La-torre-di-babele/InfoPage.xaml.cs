namespace La_torre_di_babele;
public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();
#if ANDROID
        Title = App.GetResource(La_torre_di_babele.Resource.String.Informazioni);
#else
        Title=App.d["Informazioni"] as string;
#endif
    }
    private async void OnInformazioni_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(new Uri("https://github.com/numerunix/solitario.maui"));
    }
}