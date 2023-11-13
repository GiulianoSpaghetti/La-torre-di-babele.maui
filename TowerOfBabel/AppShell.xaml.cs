namespace TowerOfBabel
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
#if ANDROID
            Home.Title = App.GetResource(TowerOfBabel.Resource.String.Home);
            Informazioni.Title=App.GetResource(TowerOfBabel.Resource.String.Informazioni);
#endif
        }
    }
}
