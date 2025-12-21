using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using TicTacTow25.Models;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.ViewModels
{
    public partial class GamePageVM : ObservableObject
    {
        private readonly GameGrid grdBoard = [];
        private readonly Game game;
        public string MyName => game.MyName;
        public string StatusMessage => game.StatusMessage;
        public string OpponentName => game.OpponentName;
        public GamePageVM(Game game, Grid board)
        {
            game.GameChanged += OnGameChanged;
            game.GameDeleted += OnGameDeleted ;
            game.DisplayChanged += OnDisplayChanged;
            grdBoard.Init(board, game.RowSize, 70,Colors.Blue);
            grdBoard.OnButtonClicked += OnButtonClicked;
            this.game = game;
            if (!game.IsHostUser)
                game.UpdateGuestUser(OnComplete);
        }

        private void OnGameDeleted(object? sender, EventArgs e)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Toast.Make(Strings.GameCanceld, ToastDuration.Long).Show();
            });
        }

        private void OnDisplayChanged(object? sender, DisplayMoveArgs e)
        {
            grdBoard.UpdateDisplay(e);
        }
        private void OnButtonClicked(object? sender, IndexedButton e)
        {
            game.Play(e.RowIndex, e.ColumnIndex, true);
        }
        private void OnGameChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(OpponentName));
            OnPropertyChanged(nameof(StatusMessage));
        }
        private void OnComplete(Task task)
        {
            if(!task.IsCompletedSuccessfully)
                Toast.Make(Strings.JoinGameErr, ToastDuration.Long, 14);
        }
        public void AddSnapshotListener()
        {
            game.AddSnapshotListener();
        }
        public void RemoveSnapshotListener()
        {
            game.RemoveSnapshotListener();
        }
    }
}
