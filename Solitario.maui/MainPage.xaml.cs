using Microsoft.Maui.Controls;
using org.altervista.numerone.framework;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace Solitario.maui;

public partial class MainPage : ContentPage
{
    int i, j, k;
    private UInt16[] vettore;
    private ulong mosse = 0;
    public MainPage()
    {
        int a = 0, b = 0;
        InitializeComponent();
        vettore = new UInt16[30];
        ElaboratoreCarteSolitario e = new ElaboratoreCarteSolitario();
        Mazzo m = new Mazzo(e);

        UInt16 c;
        Image img;
        for (UInt16 i = 0; i < 9; i++)
                {
                    c= m.GetCarta();

                    vettore[a * 10 + b] = c;
                    img=(Image) FindByName("carta" + c);
                    Applicazione.SetRow(img, a);
                    Applicazione.SetColumn(img, b);
                    img.IsVisible = true;
                    a++;
                    if (a > 2)
                    {
                        a = 0;
                        b++;
                    }
                }
              
        i = j = k = 2;
    }

    private void OnOk_Click(object sender, EventArgs e)
    {
        UInt16 inizio, fine;
        int a = 0, b = 0;
        Risultato.Text= "";
        Risultato.TextColor = Colors.White;
        if (Inizio.Text == null || Inizio.Text == "") { Risultato.Text = "La riga di inizio è vuota"; Risultato.TextColor = Colors.Red; return; }

        if (Fine.Text == null || Fine.Text == "") { Risultato.Text = "La riga di fine è vuota"; Risultato.TextColor = Colors.Red; return; }

        try
        {
            inizio = UInt16.Parse(Inizio.Text);
        }
        catch (FormatException ex)
        {
            Risultato.Text = "La riga di inizio non è intera"; Risultato.TextColor = Colors.Red; return;
        }
        inizio--;
        try
        {
            fine = UInt16.Parse(Fine.Text);
        }
        catch (FormatException ex)
        {
            Risultato.Text = "La riga di fine non è intera"; Risultato.TextColor = Colors.Red; return;
        }
        fine--;
        if (inizio > 2) { Risultato.Text = "La riga di inizio non è nel range stabilito"; Risultato.TextColor = Colors.Red; return; }
        if (fine > 2) { Risultato.Text = "La riga di fine non è nel range stabilito"; Risultato.TextColor = Colors.Red; return; }
        if (inizio == fine) { Risultato.Text = "Le righe coincidono"; Risultato.TextColor = Colors.Red; return; }
        switch (inizio)
        {
            case 0: a = i; break;
            case 1: a = j; break;
            case 2: a = k; break;
        }
        switch (fine)
        {
            case 0: b = i; break;
            case 1: b = j; break;
            case 2: b = k; break;
        }
        Image c;
        if (a==-1)
        {
            Risultato.Text = "La riga selezionata è vuota"; Risultato.TextColor = Colors.Red; return;
        }
        c = (Image)this.FindByName("carta" + vettore[inizio * 10 + a]);
        if (b!=-1 && vettore[inizio * 10 + a] > vettore[fine*10+b]) { Risultato.Text = $"Operazione non consentita"; Risultato.TextColor = Colors.Red; return; }
        vettore[fine * 10 + b+1] = vettore[inizio * 10 + a];
        vettore[inizio * 10 + a] = 0;
        Applicazione.SetRow(c, fine);
        Applicazione.SetColumn(c, b+1);
        switch (inizio)
        {
            case 0: i = --a; break;
            case 1: j = --a; break;
            case 2: k = --a; break;
        }
        switch (fine)
        {
            case 0: i = ++b; break;
            case 1: j = ++b; break;
            case 2: k = ++b; break;
        }
        mosse++;
        if (b == 8)
        {
            if (vettore[0] != 0) i = 0;
            else if (vettore[10] != 0) i = 10;
            else if (vettore[20] != 0) i = 20;
            bool continua = true;
            UInt16 x = 0;
            while (continua && x < b - 1)
            {
                if (vettore[i + x] < vettore[i+x+1])
                    continua = false;
                x++;
            }
            if (continua)
            {
                Navigation.PushAsync(new GreetingsPage(mosse));
                NuovaPartita();
            }
        }
    }

    private void OnFine_Click(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }


    private void OnNuovaPartita_Click(object sender, EventArgs e)
    {
        NuovaPartita();
    }
    private void OnInfo_Click(object sender, EventArgs e)
    {
        Navigation.PushAsync(new InfoPage());
    }
    private void NuovaPartita()
    {
        int a = 0, b = 0;
        ElaboratoreCarteSolitario el = new ElaboratoreCarteSolitario();
        Mazzo m = new Mazzo(el);

        UInt16 c;
        Image img;
        for (UInt16 i = 0; i < 9; i++)
        {
            c = m.GetCarta();

            vettore[a * 10 + b] = c;
            img = (Image)FindByName("carta" + c);
            Applicazione.SetRow(img, a);
            Applicazione.SetColumn(img, b);
            img.IsVisible = true;
            a++;
            if (a > 2)
            {
                a = 0;
                b++;
            }
        }
        i = j = k = 2;
    }



}

