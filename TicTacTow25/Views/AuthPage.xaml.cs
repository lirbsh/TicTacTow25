using TicTacTow25.ViewModels;
namespace TicTacTow25.Views;
public partial class AuthPage : ContentPage
{
	public AuthPage()
	{
		InitializeComponent();
		BindingContext = new AuthPageVM();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
#if ANDROID
        Platform.CurrentActivity!.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
#endif   
    }
}