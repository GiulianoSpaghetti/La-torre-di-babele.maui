namespace La_torre_di_babele
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
#if ANDROID
            Home.Title = App.GetResource(La_torre_di_babele.Resource.String.Home);
            Informazioni.Title=App.GetResource(La_torre_di_babele.Resource.String.Informazioni);
#endif
        }
    }
}
