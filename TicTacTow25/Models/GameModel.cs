using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    public abstract class GameModel
    {
        protected enum Actions { Changed, Deleted }
        protected Actions action = Actions.Changed;
        protected FbData fbd = new();
        protected IListenerRegistration? ilr;
        protected GameStatus _status = new();
        protected string[,]? gameBoard;
        protected IndexedButton[,]? gameButtons;
        protected string nextPlay = Strings.X;
        [Ignored]
        public EventHandler? OnGameChanged;
        [Ignored]
        public EventHandler? OnGameDeleted;
        protected abstract GameStatus Status { get;  } 
        [Ignored]
        public string StatusMessage => Status.StatusMessage;
        [Ignored]
        public string Id { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;
        public string GuestName { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public int RowSize {  get; set; }
        public bool IsFull { get; set; }
        public bool IsHostTurn { get; set; } = false;
        public List<int> Move { get; set; } = [Keys.NoMove,Keys.NoMove];
        [Ignored]
        public abstract string OpponentName { get;}
        [Ignored]
        public string MyName { get; set; } = new User().Name;
        [Ignored]
        public string RowSizeName => $"{RowSize} X {RowSize}";
        [Ignored]
        public bool IsHostUser { get; set; }
        public abstract void SetDocument(Action<System.Threading.Tasks.Task> OnComplete);
        public abstract void RemoveSnapshotListener();
        public abstract void AddSnapshotListener();
        public abstract void DeleteDocument(Action<System.Threading.Tasks.Task> OnComplete);
        public abstract void Init(Grid board);
        protected abstract void UpdateStatus();
        protected abstract void OnButtonClicked(object? sender, EventArgs e);
        protected abstract void Play(int rowIndex, int columnIndex, bool MyMove);
        protected abstract void UpdateFbMove();
    }
}
