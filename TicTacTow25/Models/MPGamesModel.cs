using Plugin.CloudFirestore;
using System.Collections.ObjectModel;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    public abstract class MPGamesModel
    {
        protected FbData fbd = new();
        protected IListenerRegistration? ilr;
        protected MPGame? _currentGame;
        protected abstract void OnGameDeleted(object? sender, EventArgs e);
        protected abstract void OnComplete(Task task);
        protected abstract void OnChange(IQuerySnapshot snapshot, Exception error);
        protected abstract void OnComplete(IQuerySnapshot qs);

        public EventHandler<MPGame>? OnGameAdded;
        public EventHandler? OnGamesChanged;
        public bool IsBusy { get; set; }
        public static ObservableCollection<int> TotalPlayers => [3, 4, 5];
        public int SelectedTotalPlayers { get; set; }
        public ObservableCollection<MPGame>? GamesList { get; set; } = [];
        public MPGame? CurrentGame { get => _currentGame; set => _currentGame = value; }
        public abstract void RemoveSnapshotListener();
        public abstract void AddSnapshotListener();
        public abstract void AddGame();
    }
}