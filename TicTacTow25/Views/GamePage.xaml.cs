using TicTacTow25.ModelsLogic;
using TicTacTow25.ViewModels;

namespace TicTacTow25.Views;


public partial class GamePage : ContentPage
{
	public GamePage(Game game)
	{
		InitializeComponent();
		BindingContext = new GamePageVM(game);
	}
}