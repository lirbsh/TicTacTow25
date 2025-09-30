namespace TicTacTow25.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = new ViewModels.RegisterPageVM();
    }
}