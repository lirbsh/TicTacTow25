using TicTacTow25.ViewModels;

namespace TicTacTow25.Views;

public partial class MPGamesPage : ContentPage
{
	private MPGamesPageVM mpgpVM = new();
    public MPGamesPage()
	{
		InitializeComponent();
		BindingContext = mpgpVM;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        mpgpVM.AddSnapshotListener();
    }
    protected override void OnDisappearing() 
    {
        mpgpVM.RemoveSnapshotListener();
        base.OnDisappearing();
    }
}