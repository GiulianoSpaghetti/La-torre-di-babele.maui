namespace La_torre_di_babele;


public partial class AppShellWindows : Shell
{
    public AppShellWindows()
    {
        InitializeComponent();
#if ANDROID
#else
        Home.Title = App.d["Home"] as string;
#endif
    }
}
