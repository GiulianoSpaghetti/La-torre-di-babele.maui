using CommunityToolkit.Maui.Alerts;
using org.altervista.numerone.framework;

namespace La_torre_di_babele
{
    public partial class MainPage : ContentPage
    {
        int i, j, k;
        private UInt16[] vettore;
        private ulong mosse = 0;
        private CancellationTokenSource cancellationTokenSource;
        public MainPage()
        {
            int a = 0, b = 0;
            InitializeComponent();
            cancellationTokenSource = new CancellationTokenSource();
            vettore = new UInt16[30];
            ElaboratoreCarteSolitario e = new ElaboratoreCarteSolitario();
            Mazzo m = new Mazzo(e);
#if ANDROID
            Prompt.Text = App.GetResource(La_torre_di_babele.Resource.String.Prompt);
            Prompt1.Text=App.GetResource(La_torre_di_babele.Resource.String.Prompt1);
            Ok.Text=App.GetResource(La_torre_di_babele.Resource.String.Ok);
#else
            Prompt.Text = App.d["Prompt"] as string;
            Prompt1.Text = App.d["Prompt1"] as string;
            Ok.Text=App.d["Ok"] as string;
            mnFile.Text=App.d["File"] as string;
            mnNuovaPartita.Text=App.d["NuovaPartita"] as string;
            mnEsci.Text=App.d["Esci"] as string;
            mnInfo.Text=App.d["Info"] as string;
            mnInformazioni.Text=App.d["Informazioni"] as string;
#endif
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

        private void OnOk_Click(object sender, EventArgs e)
        {
            UInt16 inizio, fine;
            int a = 0, b = 0;
            if (Inizio.Text == null || Inizio.Text == "") {
                Snackbar.Make(
#if ANDROID
                    App.GetResource(La_torre_di_babele.Resource.String.RigaInizioVuota)
#else
                    App.d["RigaInizioVuota"] as string
#endif
                    ).Show(cancellationTokenSource.Token); return; }

            if (Fine.Text == null || Fine.Text == "") { Snackbar.Make(
#if ANDROID
                    App.GetResource(La_torre_di_babele.Resource.String.RigaFineVuota)
#else
                    App.d["RigaFineVuota"] as string
#endif
                    ).Show(cancellationTokenSource.Token); return; }

            try
            {
                inizio = UInt16.Parse(Inizio.Text);
            }
            catch (FormatException ex)
            {
                Snackbar.Make(
#if ANDROID
                    App.GetResource(La_torre_di_babele.Resource.String.RigaInizioNonIntera)
#else
                    App.d["RigaInizioNonIntera"] as string
#endif
                    ).Show(cancellationTokenSource.Token); return;
            }
            inizio--;
            try
            {
                fine = UInt16.Parse(Fine.Text);
            }
            catch (FormatException ex)
            {
                Snackbar.Make(
#if ANDROID
                    App.GetResource(La_torre_di_babele.Resource.String.RigaFineNonIntera)
#else
                    App.d["RigaFineNonIntera"] as string
#endif
                    ).Show(cancellationTokenSource.Token); return;
            }
            fine--;
            if (inizio > 2) { Snackbar.Make(
#if ANDROID
                    App.GetResource(La_torre_di_babele.Resource.String.RigaInizioFuoriRange)
#else
                    App.d["RigaInizioFuoriRange"] as string
#endif
                    ).Show(cancellationTokenSource.Token); return; }
            if (fine > 2) { Snackbar.Make(
#if ANDROID
                    App.GetResource(La_torre_di_babele.Resource.String.RigaFineFuoriRange)
#else
                    App.d["RigaFineFuoriRange"] as string
#endif
                    ).Show(cancellationTokenSource.Token); return; }
            if (inizio == fine) { Snackbar.Make(
#if ANDROID
                    App.GetResource(La_torre_di_babele.Resource.String.RigheUguali)
#else
                    App.d["RigheUguali"] as string
#endif
                    ).Show(cancellationTokenSource.Token); return; }
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
            if (a == -1)
            {
                Snackbar.Make(
#if ANDROID
                    App.GetResource(La_torre_di_babele.Resource.String.RigaVuota)
#else
                    App.d["RigaVuota"] as string
#endif
                    ).Show(cancellationTokenSource.Token); return;
            }
            c = (Image)this.FindByName("carta" + vettore[inizio * 10 + a]);
            if (b != -1 && vettore[inizio * 10 + a] > vettore[fine * 10 + b]) { Snackbar.Make(
#if ANDROID
                    App.GetResource(La_torre_di_babele.Resource.String.OperazioneInvalida)
#else
                    App.d["OperazioneInvalida"] as string
#endif
            ).Show(cancellationTokenSource.Token); return; }
            vettore[fine * 10 + b + 1] = vettore[inizio * 10 + a];
            vettore[inizio * 10 + a] = 0;
            Applicazione.SetRow(c, fine);
            Applicazione.SetColumn(c, b + 1);
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
                    if (vettore[i + x] < vettore[i + x + 1])
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
}
