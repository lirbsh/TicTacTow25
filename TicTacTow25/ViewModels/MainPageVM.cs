using System.Collections.ObjectModel;
using System.Windows.Input;
using TicTacTow25.Models;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.ViewModels
{
    internal class MainPageVM:ObservableObject
    {
        private Games games = new();
        public  IList<GameSize>? GameSizes { get; set; }
        public GameSize? SelectedGameSize { get; set; }
        public ICommand AddGameCommand => new Command(AddGame);

        private void AddGame()
        {
           
        }
        public IList<Game>? GamesList => games.GamesList;
        public MainPageVM() 
        {
            GameSizes = [new GameSize(3), new GameSize(4), new GameSize(5)];
        }
    }
}
