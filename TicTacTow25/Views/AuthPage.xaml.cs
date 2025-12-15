using TicTacTow25.ViewModels;
namespace TicTacTow25.Views;
public partial class AuthPage : ContentPage
{
	public AuthPage()
	{
		InitializeComponent();
		BindingContext = new AuthPageVM();
    }
}