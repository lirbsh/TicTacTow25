using TicTacTow25.Views;

namespace TicTacTow25
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AuthPage();//  new AppShell();
        }
    }
}
