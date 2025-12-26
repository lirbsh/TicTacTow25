using CommunityToolkit.Mvvm.Messaging;
using Plugin.CloudFirestore;
using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class Game : GameModel
    {
        public override string OpponentName => IsHostUser ? GuestName : HostName;

        public Game() { RegisterTimer(); }
        public Game(GameSize selectedGameSize)
        {
            RegisterTimer();
            Created = DateTime.Now;
            HostName = new User().Name;
            IsHostUser = true;
            RowSize = selectedGameSize.Size;
            InitBoardAndStatus();
        }

        private void RegisterTimer()
        {
            WeakReferenceMessenger.Default.Register<AppMessage<long>>(this, (r, m) =>
            {
                OnMessageReceived(m.Value);
            });
        }

        private void OnMessageReceived(long timeLeft)
        {
            TimeLeft = timeLeft == Keys.FinishedSignal ? Strings.TimeUp : double.Round(timeLeft / 1000, 1).ToString();
            TimeLeftChanged?.Invoke(this, EventArgs.Empty);
        }

       
        public override void InitBoardAndStatus()
        {
            gameBoard = new string[RowSize, RowSize];
            UpdateStatus();
        }
        protected override void UpdateStatus()
        {
            _status.CurrentStatus = IsHostUser && IsHostTurn || !IsHostUser && !IsHostTurn ?
                GameStatus.Statuses.Play : GameStatus.Statuses.Wait;
        }
        protected override void UpdateFbJoinGame(Action<Task> OnComplete)
        {
            Dictionary<string, object> dict = new()
            {
                { nameof(IsFull), IsFull },
                { nameof(GuestName), GuestName }
            };
            action = Actions.Changed;
            fbd.UpdateFields(Keys.GamesCollection, Id, dict, OnComplete);
        }
        protected override void OnComplete(Task task)
        {
            if (task.IsCompletedSuccessfully)
                if (action == Actions.Deleted)
                    GameDeleted?.Invoke(this, EventArgs.Empty);
                else
                    GameChanged?.Invoke(this, EventArgs.Empty);
        }
       
        public override void Play(int rowIndex, int columnIndex, bool MyMove)
        {
            if (string.IsNullOrEmpty(gameBoard![rowIndex, columnIndex]))
                if (_status.CurrentStatus == GameStatus.Statuses.Play)
                {
                    DisplayMoveArgs args = new(rowIndex, columnIndex, nextPlay);
                    DisplayChanged?.Invoke(this, args);
                    gameBoard![rowIndex, columnIndex] = nextPlay;
                    nextPlay = nextPlay == Strings.X ? Strings.O : Strings.X;
                    if (MyMove)
                    {
                        Move[0] = rowIndex;
                        Move[1] = columnIndex;
                        _status.ChangeStatus();
                        IsHostTurn = !IsHostTurn;
                        UpdateFbMove();
                    }
                    else
                        GameChanged?.Invoke(this, EventArgs.Empty);
                }
        }
        protected override void UpdateFbMove()
        {
            Dictionary<string, object> dict = new()
            {
                { nameof(Move), Move },
                { nameof(IsHostTurn), IsHostTurn }
            };
            fbd.UpdateFields(Keys.GamesCollection, Id, dict, OnComplete);
        }
        protected override void OnChange(IDocumentSnapshot? snapshot, Exception? error)
        {
            Game? updatedGame = snapshot?.ToObject<Game>();
            if (updatedGame != null)
            {
                IsFull = updatedGame.IsFull;
                GuestName = updatedGame.GuestName;
                GameChanged?.Invoke(this, EventArgs.Empty);
                IsHostTurn = updatedGame.IsHostTurn;
                UpdateStatus();
                if (_status.CurrentStatus == GameStatus.Statuses.Play)
                {
                    WeakReferenceMessenger.Default.Send(new AppMessage<TimerSettings>(timerSettings));
                    if (updatedGame.Move[0] != Keys.NoMove)
                        Play(updatedGame.Move[0], updatedGame.Move[1], false);
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(new AppMessage<bool>(true));
                    TimeLeft = string.Empty;
                    TimeLeftChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    GameDeleted?.Invoke(this, EventArgs.Empty);
                    Shell.Current.Navigation.PopAsync();
                });
            }
        }
        public override void SetDocument(Action<Task> OnComplete)
        {
            Id = fbd.SetDocument(this, Keys.GamesCollection, Id, OnComplete);
        }
        public override void UpdateGuestUser(Action<Task> OnComplete)
        {
            IsFull = true;
            GuestName = MyName;
            UpdateFbJoinGame(OnComplete);
        }
        public override void AddSnapshotListener()
        {
            ilr = fbd.AddSnapshotListener(Keys.GamesCollection, Id, OnChange);
        }
        public override void RemoveSnapshotListener()
        {
            ilr?.Remove();
            action = Actions.Deleted;
            DeleteDocument(OnComplete);
        }
        public override void DeleteDocument(Action<Task> OnComplete)
        {
            fbd.DeleteDocument(Keys.GamesCollection, Id, OnComplete);
        }
    }
}