using System.Collections.ObjectModel;
using System.Windows.Input;
using TicTacTow25.Models;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.ViewModels
{
    internal partial class MainPageVM:ObservableObject
    {
        private readonly Games games = new();
        public bool IsBusy => games.IsBusy;
        public  ObservableCollection<GameSize>? GameSizes { get => games.GameSizes; set => games.GameSizes = value; }
        public GameSize SelectedGameSize { get; set; } = new GameSize();
        public ICommand AddGameCommand => new Command(AddGame);

        private void AddGame()
        {
            games.AddGame(SelectedGameSize);
            OnPropertyChanged(nameof(IsBusy));
        }
        public ObservableCollection<Game>? GamesList => games.GamesList;
        public MainPageVM()
        {
            games.OnGameAdded += OnGameAdded;
            games.OnGamesChanged += OnGamesChanged;
        }

        private void OnGamesChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(GamesList));
        }

        private void OnGameAdded(object? sender, bool e)
        {
            OnPropertyChanged(nameof(IsBusy));
        }
        internal void AddSnapshotListener()
        {
            games.AddSnapshotListener();
        }

        internal void RemoveSnapshotListener()
        {
            games.RemoveSnapshotListener();
        }
    }
}
