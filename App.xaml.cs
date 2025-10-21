namespace SomethingToDoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navPage = new NavigationPage(new MainPage());
            navPage.BackgroundColor = Colors.PeachPuff;
            navPage.BarTextColor = Colors.White;

            MainPage = new AppShell();
        }
    }
}
