namespace TowerOfBabel
{
    public partial class App : Application
    {
        public static string piattaforma;
        public static ResourceDictionary d;
        public App()
        {
            InitializeComponent();
            try { d = Resources[System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName] as ResourceDictionary; }
            catch (Exception ex) { d = Resources["en"] as ResourceDictionary; }

#if ANDROID
            MainPage = new AppShell();
#else
            MainPage = new AppShellWindows();
#endif
            piattaforma = DeviceInfo.Current.Model;
            if (piattaforma == "System Product Name")
                piattaforma = "Windows " + DeviceInfo.Current.VersionString;

        }

    }
}
