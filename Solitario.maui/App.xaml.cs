namespace Solitario.maui;

public partial class App : Application
{
    public static string piattaforma;
    public App()
	{
		InitializeComponent();

#if WINDOWS
        MainPage = new AppShellWindows();
#else
        MainPage = new AppShell();
#endif
        piattaforma = DeviceInfo.Current.Model;
        if (piattaforma == "System Product Name")
            piattaforma = "PC";

    }
}
