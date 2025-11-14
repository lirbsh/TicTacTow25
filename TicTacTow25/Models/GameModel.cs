using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    public abstract class GameModel
    {
        protected FbData fbd = new();
        protected IListenerRegistration? ilr;
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
        public abstract void InitGrid(Grid board);
    }
}
