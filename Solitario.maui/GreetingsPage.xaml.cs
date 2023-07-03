namespace Solitario.maui;

public partial class GreetingsPage : ContentPage
{
    private ulong mosse;
	public GreetingsPage(ulong mosse)
	{
		InitializeComponent();
        this.mosse = mosse;
        msgfine.Text = $"Hai finito il solitario in {mosse} mosse. Vuoi fare una nuova partita?";

    }
    private void OnFine_Click(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }


    private async void greetingsShare_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(new Uri($"https://twitter.com/intent/tweet?text=Ho%20completato%20la%20torre%20di%20babele%20di%20numerone%20in%20{mosse}%20mosse%20su%20piattaforma%20{App.piattaforma}&url=https%3A%2F%2Fgithub.com%2Fnumerunix%2FSolitario.maui"));
        condividi.IsEnabled = false;
    }

}