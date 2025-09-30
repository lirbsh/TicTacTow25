namespace TicTacTow25.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new ViewModels.LoginPageVM();
    }
}