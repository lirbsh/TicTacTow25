using System.Xml;
using TicTacTow25.ViewModels;
namespace TicTacTow25.Views;
public partial class AuthPage : ContentPage
{
    private readonly AuthPageVM apVM = new();
    public AuthPage()
    {
        InitializeComponent();
        BindingContext = apVM;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
#if ANDROID
        Platform.CurrentActivity!.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
#endif


    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        apVM.StartAnimations();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        apVM.StopAnimations();
    }
}