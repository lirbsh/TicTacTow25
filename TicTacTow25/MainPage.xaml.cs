using TicTacTow25.ViewModels;

namespace TicTacTow25
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageVM();
        }
    }

}
