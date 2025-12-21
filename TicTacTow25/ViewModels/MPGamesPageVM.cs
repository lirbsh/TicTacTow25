using System.Collections.ObjectModel;
using System.Windows.Input;
using TicTacTow25.Models;
using TicTacTow25.ModelsLogic;
using TicTacTow25.Views;

namespace TicTacTow25.ViewModels
{
    public partial class MPGamesPageVM : ObservableObject
    {
        private readonly MPGames mpGames = new();
        public ICommand AddGameCommand { get; } 

        private bool CanAddGame()
        {
            return SelectedTotalPlayers > 0;
        }

        public bool IsBusy => mpGames.IsBusy;
        public static ObservableCollection<int> TotalPlayers => MPGames.TotalPlayers;
        public int SelectedTotalPlayers
        {
            get => mpGames.SelectedTotalPlayers;
            set
            {
                mpGames.SelectedTotalPlayers = value;
                (AddGameCommand as Command)?.ChangeCanExecute();
            }
        }
        public ObservableCollection<MPGame>? GamesList => mpGames.GamesList;
        public MPGame? SelectedItem
        {
            get => mpGames.CurrentGame;

            set
            {
                if (value != null)
                {
                    mpGames.CurrentGame = value;
                    value.JoinGame();
                    MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        Shell.Current.Navigation.PushAsync(new MPGamePage(value), true);
                    });
                }
            }
        }
        public MPGamesPageVM()
        {
            AddGameCommand = new Command(AddGame, CanAddGame);
            mpGames.OnGameAdded += OnGameAdded;
            mpGames.OnGamesChanged += OnGamesChanged;

        }
        private void OnGamesChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(GamesList));
        }
        private void OnGameAdded(object? sender, MPGame game)
        {
            OnPropertyChanged(nameof(IsBusy));
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Shell.Current.Navigation.PushAsync(new MPGamePage(game), true);
            });
        }
        private void AddGame()
        {
            if (!IsBusy)
            {
                mpGames.AddGame();
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        public void AddSnapshotListener()
        {
            mpGames.AddSnapshotListener();
        }
        public void RemoveSnapshotListener()
        {
            mpGames.RemoveSnapshotListener();
        }
    }
}
