using System.Collections.ObjectModel;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    internal class GamesModel
    {
        protected FbData fbd = new();
        public bool IsBusy { get; set; }
        public ObservableCollection<Game>? GamesList {  get; set; }
        public IList<GameSize>? GameSizes { get; set; } = [new GameSize(3), new GameSize(4), new GameSize(5)];
        public EventHandler<bool>? OnGameAdded;
    }
}
