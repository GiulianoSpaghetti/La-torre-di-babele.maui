using org.altervista.numerone.framework;

namespace La_torre_di_babele
{
    public partial class MainPage : ContentPage
    {
        private int a,b;
        private Image img;
        private UInt16[] vettore;
        private UInt16 c;
        private ulong mosse = 0;
        private TapGestureRecognizer gesture, gesture1;
        public MainPage()
        {
            UInt16 a = 0, b = 0, c=10;
            InitializeComponent();
            vettore = new UInt16[27];
            ElaboratoreCarteSolitario e = new ElaboratoreCarteSolitario();
            Mazzo m = new Mazzo(e);
#if ANDROID
            ;
#else
            mnFile.Text=App.d["File"] as string;
            mnNuovaPartita.Text=App.d["NuovaPartita"] as string;
            mnEsci.Text=App.d["Esci"] as string;
            mnInfo.Text=App.d["Info"] as string;
            mnInformazioni.Text=App.d["Informazioni"] as string;
#endif
            gesture = new TapGestureRecognizer();
            gesture.Tapped += Image_Tapped;
            gesture1 = new TapGestureRecognizer();
            gesture1.Tapped += Image_Tapped1;
            for (UInt16 i = 0; i < 9; i++)
            {
                c = m.GetCarta();

                vettore[a * 9 + b] = c;
                img = (Image)FindByName($"carta{c}");
                img.GestureRecognizers.Add(gesture);
                Applicazione.SetColumn(img, a);
                Applicazione.SetRow(img, b);
                img.IsVisible = true;
                a++;
                if (a > 2)
                {
                    a = 0;
                    b++;
                }
            }
            c = 10;
            for (UInt16 j = 0; j < 3; j++)
                for (UInt16 i = 3; i < 9; i++)
                    vettore[j * 9 + i] = c++;
            for (UInt16 i = 10; i < 28; i++) { 
                img = (Image)FindByName($"carta{i}");
                img.GestureRecognizers.Add(gesture1);
            }
        }

        private void Image_Tapped(object Sender, EventArgs arg)
        {
            Image im;
            UInt16 i, j;
            img = (Image)Sender;
            for (i = 1; i < 9; i++)
            {
                im = (Image)FindByName("carta" + i);
                if (im.Id == img.Id)
                {
                    break;
                }
            }
            for (j=0; j<28; j++)
            {
                if (vettore[j] == i)
                    break;
            }
            if (vettore[j + 1] > 9)
                a = j;
            else
                img = null;
        }
        private void Image_Tapped1(object Sender, EventArgs arg)
        {
            mosse++;
            bool found = false;
/*            if (img == null)
                return;*/
            UInt16 i=0, j, k;
            Image im0 = (Image)Sender, im1=null;
            for (i = 10; i < 27 && !found; i++)
            {
               im1 = (Image)FindByName("carta" + i);
               if (im0.Id == im1.Id)
               {
                   found = true;
               }
            }
            found = false;
            --i;
            for (j = 0; j < 27 && !found; j++)
                if (vettore[j] == i)
                    found = true;
            --j;
            --j;
            b = j;
            if (b == UInt16.MaxValue)
            {
                c = vettore[0];
                vettore[0] = vettore[a];
                vettore[a] = c;
                Applicazione.SetColumn(im1, 3);
                Applicazione.SetRow(im1, 0);
                Applicazione.SetRow(img, 0);
                Applicazione.SetColumn(img, 0);
                Applicazione.SetColumn(im1, a / 9);
                Applicazione.SetRow(im1, a % 9);
            }
            else if (b %9==8)
            {
                c = vettore[b+1];
                vettore[b+1] = vettore[a];
                vettore[a] = c;
                Applicazione.SetColumn(im1, 3);
                Applicazione.SetRow(im1, 0);
                Applicazione.SetRow(img, 0);
                Applicazione.SetColumn(img, (b+1)/9);
                Applicazione.SetColumn(im1, a / 9);
                Applicazione.SetRow(im1, a % 9);
            }
            else if ((vettore[a] < vettore[b] && vettore[b]<10) || vettore[b]>9 && vettore[b-1]<10)
            {
                c = vettore[b + 1];
                vettore[b + 1] = vettore[a];
                vettore[a] = c;
                Applicazione.SetColumn(im1,3);
                Applicazione.SetRow(im1, 0);
                Applicazione.SetRow(img, (b+1)%9);
                Applicazione.SetColumn(img, (b+1) / 9);
                Applicazione.SetColumn(im1, a/9);
                Applicazione.SetRow(im1, a%9);
                if (b % 9 == 7)
                {
                    bool continua = true;
                    UInt16 x = 0;
                    if (vettore[0] == 9)
                        i = 0;
                    else if (vettore[10] == 9)
                        i = 10;
                    else if (vettore[20] == 9)
                        i = 20;
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
        }

        /*        private void OnOk_Click(object sender, EventArgs e)
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
                }*/

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

                vettore[a * 9 + b] = c;
                img = (Image)FindByName($"carta{c}");
                Applicazione.SetColumn(img, a);
                Applicazione.SetRow(img, b);
                img.IsVisible = true;
                a++;
                if (a > 2)
                {
                    a = 0;
                    b++;
                }
            }
            c = 10;
            for (UInt16 j = 0; j < 3; j++)
                for (UInt16 i = 3; i < 9; i++)
                    vettore[j * 9 + i] = c++;
            a = 3; b = 0;
            for (UInt16 i = 10; i < 28; i++)
            {
                img = (Image)FindByName($"carta{i}");
                Applicazione.SetColumn(img, b);
                Applicazione.SetRow(img, a);
                if (i % 5 == 0)
                {
                    a = 3;
                    b++;
                }
                else
                    a++;
            }
        }
    }
}
