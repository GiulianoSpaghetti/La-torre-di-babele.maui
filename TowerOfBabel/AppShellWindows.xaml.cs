namespace TowerOfBabel;


public partial class AppShellWindows : Shell
{
    public AppShellWindows()
    {
        InitializeComponent();
        Home.Title = App.d["Home"] as string;
    }
}
