using TicTacTow25.ModelsLogic;
using TicTacTow25.ViewModels;

namespace TicTacTow25.Views;


public partial class GamePage : ContentPage
{
    private readonly GamePageVM gpVM;
    public GamePage(Game game)
	{
		InitializeComponent();
        gpVM = new GamePageVM(game,grdBoard);
        BindingContext = gpVM;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        gpVM.AddSnapshotListener();
    }

    protected override void OnDisappearing()
    {
        gpVM.RemoveSnapshotListener();
        base.OnDisappearing();
    }
}
