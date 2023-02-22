namespace Solitario.maui;

public partial class App : Application
{
    public static string piattaforma;

    public App()
	{
		InitializeComponent();

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            MainPage = new AppShell();
            piattaforma = "Android";
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            MainPage = new AppShellWindows();
            piattaforma = "Windows NT";
        }
        else
        {
            MainPage = new AppShell();
            piattaforma = "Unknown";
        }
    }
}
