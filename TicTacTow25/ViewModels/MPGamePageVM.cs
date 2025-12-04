using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;
using TicTacTow25.Models;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.ViewModels
{
    public partial class MPGamePageVM: ObservableObject
    {
        private readonly MPGame game;
        private readonly List<Label> lstOponnentsLabels = [];
        public string MyName => game.MyName;
        public string Message { get => game.Message; set => game.Message = value; }
        public string MyMessage { get => game.MyMessage; set => game.MyMessage = value; }
        public bool IsMyTurn => game.IsMyTurn();
        public ICommand SendMessageCommand { get; }

        public MPGamePageVM(MPGame game,Grid grdOponnents)
        {
            this.game = game;
            game.OnGameChanged += OnGameChanged;
            game.OnGameDeleted += OnGameDeleted;
            SendMessageCommand = new Command(SendMessage,CanSendMessage);
            InitOponnentsGrid(grdOponnents);
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
            ((Command)SendMessageCommand).ChangeCanExecute();
            OnPropertyChanged(nameof(Message));
            OnPropertyChanged(nameof(IsMyTurn));
        }

        private void DisplayOponnentsNames()
        {
            int lblIndex = 0;
            for (int i = 0; i < game.MyIndex; i++)
            {
                lstOponnentsLabels[lblIndex].Text = game.PlayersNames[i];
                lstOponnentsLabels[lblIndex++].BackgroundColor = i== game.NextPlay ? Colors.Yellow : Colors.Cyan;
            }
            for (int i = game.MyIndex + 1; i < game.PlayersNames.Count; i++)
            {
                lstOponnentsLabels[lblIndex].Text = game.PlayersNames[i];
                lstOponnentsLabels[lblIndex++].BackgroundColor = i == game.NextPlay ? Colors.Yellow : Colors.Cyan;
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
                    FontSize = 16,
                    Margin = new Thickness(5),
                    Padding = new Thickness(12)
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
