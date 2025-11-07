using Plugin.CloudFirestore;
using System.Collections.ObjectModel;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    public class GamesModel
    {
        protected FbData fbd = new();
        protected IListenerRegistration? ilr;
        protected Game? currentGame;

        public bool IsBusy { get; set; }
        public Game? CurrentGame { get => currentGame; set => currentGame = value; }
        public ObservableCollection<Game>? GamesList { get; set; } = [];
        public ObservableCollection<GameSize>? GameSizes { get; set; } = [new GameSize(3), new GameSize(4), new GameSize(5)];
        public GameSize SelectedGameSize { get; set; } = new GameSize();

        public EventHandler<Game>? OnGameAdded;
        public EventHandler? OnGamesChanged;
    }
}
