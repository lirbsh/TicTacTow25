using Plugin.CloudFirestore;
using System.Collections.ObjectModel;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    public abstract class GamesModel
    {
        protected FbData fbd = new();
        protected IListenerRegistration? ilr;
        protected Game? _currentGame;

        public bool IsBusy { get; set; }
        public Game? CurrentGame { get => _currentGame; set => _currentGame = value; }
        public ObservableCollection<Game>? GamesList { get; set; } = [];
        public ObservableCollection<GameSize>? GameSizes { get; set; } = [new GameSize(3), new GameSize(4), new GameSize(5)];
        public GameSize SelectedGameSize { get; set; } = new();

        public EventHandler<Game>? OnGameAdded;
        public EventHandler? OnGamesChanged;
        public abstract void RemoveSnapshotListener();
        public abstract void AddSnapshotListener();
        public abstract void AddGame();
        protected abstract void OnGameDeleted(object? sender, EventArgs e);
        protected abstract void OnComplete(Task task);
        protected abstract void OnChange(IQuerySnapshot snapshot, Exception error);
        protected abstract void OnComplete(IQuerySnapshot qs);
    }
}
