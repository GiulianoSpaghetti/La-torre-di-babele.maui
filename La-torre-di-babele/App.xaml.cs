namespace La_torre_di_babele
{
    public partial class App : Application
    {
        public static string piattaforma;
#if ANDROID
#else
        public static ResourceDictionary d;
#endif
        public App()
        {
            InitializeComponent();

#if ANDROID
            MainPage = new AppShell();
#else
            try { d = Resources[System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName] as ResourceDictionary; }
            catch (Exception ex) { d = Resources["en"] as ResourceDictionary; }
            MainPage = new AppShellWindows();
#endif
            piattaforma = DeviceInfo.Current.Model;
            if (piattaforma == "System Product Name")
                piattaforma = "Windows " + DeviceInfo.Current.VersionString;

        }

#if ANDROID
    public static System.String GetResource(int id)
    {
        return Android.App.Application.Context.Resources.GetString(id);

    }
#endif

    }
}
