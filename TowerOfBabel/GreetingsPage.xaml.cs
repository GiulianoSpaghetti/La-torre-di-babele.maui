namespace TowerOfBabel;
public partial class GreetingsPage : ContentPage
{
    private ulong mosse;
	public GreetingsPage(ulong mosse)
	{
		InitializeComponent();
        this.mosse = mosse;
#if ANDROID
        Title=App.GetResource(TowerOfBabel.Resource.String.PartitaFinita);
        msgfine.Text = $"{App.GetResource(TowerOfBabel.Resource.String.HaiCompletato)} {mosse} {App.GetResource(TowerOfBabel.Resource.String.GiocareAncora)}?";
#else
        Title = App.d["PartitaFinita"] as string;
        msgfine.Text = $"{App.d["HaiCompletato"]} {mosse} {App.d["GiocareAncora"]}?";
#endif

    }
    private void OnFine_Click(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }


    private async void greetingsShare_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(new Uri($"https://twitter.com/intent/tweet?text=I%20have%20completed%20numerone%20babel%20tower%20di%20numerone%20in%20{mosse}%20moves%20on%20platform%20{App.piattaforma}&url=https%3A%2F%2Fgithub.com%2Fnumerunix%2FSolitario.maui"));
        condividi.IsEnabled = false;
    }

}