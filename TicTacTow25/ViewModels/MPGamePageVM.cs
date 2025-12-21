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
        private readonly List<Label> lstOponnentsLabels = [];
        public string MyName => game.MyName;
        public string Message { get => game.Message; set => game.Message = value; }
        public string MyMessage { get => game.MyMessage; set => game.MyMessage = value; }
        public bool IsMyTurn => game.IsMyTurn();
        public ICommand SendMessageCommand { get; }
        public MPGamePageVM(MPGame game,Grid grdOponnents, Grid grdBoard)
        {
            this.game = game;
            this.grdBoard.Init(grdBoard, 5, 15,Colors.Cyan);
            game.OnGameChanged += OnGameChanged;
            game.OnGameDeleted += OnGameDeleted;
            game.OnGameError += OnGameError;
            SendMessageCommand = new Command(SendMessage,CanSendMessage);
            InitOponnentsGrid(grdOponnents);
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
                Toast.Make(Strings.GameCanceld, CommunityToolkit.Maui.Core.ToastDuration.Long, 14).Show();
            });
        }
        private void OnGameChanged(object? sender, EventArgs e)
        {
            DisplayOponnentsNames();
            UpdatGameGrid();
            ((Command)SendMessageCommand).ChangeCanExecute();
            OnPropertyChanged(nameof(Message));
            OnPropertyChanged(nameof(IsMyTurn));
        }
        private void UpdatGameGrid()
        {
            for(int i = 0; i < game.PlayersCount; i++)
                grdBoard.UpdateButton(game.GetPlayerPosition(i), game.GetPlayerColor(i));
        }

        private void DisplayOponnentsNames()
        {
            int lblIndex = 0;
            for (int i = 0; i < game.MyIndex; i++)
            {
                lstOponnentsLabels[lblIndex].Text = game.GetPlayerName(i);
                lstOponnentsLabels[lblIndex++].BackgroundColor = i == game.NextPlay ? Colors.Yellow : Colors.Cyan;
            }
            for (int i = game.MyIndex + 1; i < game.PlayersCount; i++)
            {
                lstOponnentsLabels[lblIndex].Text = game.GetPlayerName(i);
                lstOponnentsLabels[lblIndex++].BackgroundColor = game.IsOponnentTurn(i) ? Colors.Yellow : Colors.Cyan;
            }
        }
        private void InitOponnentsGrid(Grid grdOponnents)
        {
            int oponnentsCount = game.TotalPlayers - 1;
            for (int i = 0; i < oponnentsCount; i++)
            {
                grdOponnents.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                lstOponnentsLabels.Add(new Label
                {
                    Text = Strings.Waiting,
                    FontSize = 14,
                    Margin = new Thickness(1),
                    Padding = new Thickness(1)
                });
                grdOponnents.Add(lstOponnentsLabels[i], i, 0);
            }
        }
        internal void AddSnapshotListener()
        {
            game.AddSnapshotListener();
        }
        internal void RemoveSnapshotListener()
        {
            game.RemoveSnapshotListener();
        }
    }
}
