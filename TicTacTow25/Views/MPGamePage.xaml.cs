using TicTacTow25.ModelsLogic;
using TicTacTow25.ViewModels;

namespace TicTacTow25.Views;

public partial class MPGamePage : ContentPage
{
    private object? SOToRestore { get; set; }
    private readonly MPGamePageVM mpgpVM;
    public MPGamePage(MPGame game)
	{
        InitializeComponent();
        mpgpVM = new MPGamePageVM(game, grdOponnents);
		BindingContext = mpgpVM;

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        mpgpVM.AddSnapshotListener();
#if ANDROID
        if (Platform.CurrentActivity != null)
        {
            SOToRestore = Platform.CurrentActivity.RequestedOrientation;
            Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
        }
#endif
    }

    protected override void OnDisappearing()
    {
        mpgpVM.RemoveSnapshotListener();
#if ANDROID
        if (Platform.CurrentActivity != null)
            if (SOToRestore is Android.Content.PM.ScreenOrientation SO) Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;

#endif
        base.OnDisappearing();
    }
}
