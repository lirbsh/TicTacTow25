using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Windows.Input;
using TicTacTow25.Models;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.ViewModels
{
    public partial class MPGamePageVM: ObservableObject
    {
        private readonly GameGrid grdBoard = [];
        private readonly MPGame game;
        private readonly OponnentsGrid grdOponnents;
        public string MyName => game.MyName;
        public string Message { get => game.Message; set => game.Message = value; }
        public string MyMessage { get => game.MyMessage; set => game.MyMessage = value; }
        public bool IsMyTurn => game.IsMyTurn();
        public ICommand SendMessageCommand { get; }
        public MPGamePageVM(MPGame game,Grid grdOponnents, Grid grdBoard)
        {
            this.game = game;
            this.grdOponnents = new OponnentsGrid(grdOponnents, game);
            this.grdBoard.Init(grdBoard, 5, 15,Colors.Cyan);
            this.grdBoard.ButtonClicked += OnButtonClicked;
            game.GameChanged += OnGameChanged;
            game.GameDeleted += OnGameDeleted;
            game.GameError += OnGameError;
            SendMessageCommand = new Command(SendMessage,CanSendMessage);
        }
        private void OnButtonClicked(object? sender, IndexedButton e)
        {
            game.Play(e.RowIndex, e.ColumnIndex);
        }
        private void OnGameError(object? sender, EventArgs e)
        {
            Toast.Make(Strings.GameError, ToastDuration.Long, 14).Show();
        }
        private bool CanSendMessage()
        {
            return IsMyTurn;
        }
        private void SendMessage()
        {
            game.SendMessage();
        }
        private void OnGameDeleted(object? sender, EventArgs e)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Shell.Current.Navigation.PopAsync();
                Toast.Make(Strings.GameCanceld, ToastDuration.Long, 14).Show();
            });
        }
        private void OnGameChanged(object? sender, EventArgs e)
        {
            grdOponnents.DisplayOponnentsNames();
            UpdatGameGrid();
            ((Command)SendMessageCommand).ChangeCanExecute();
            OnPropertyChanged(nameof(Message));
            OnPropertyChanged(nameof(IsMyTurn));
        }
        private void UpdatGameGrid()
        {
            grdBoard.RestoreColors();
            for (int i = 0; i < game.PlayersCount; i++)
                grdBoard.UpdateButton(game.GetPlayerPosition(i), game.GetPlayerColor(i));
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
