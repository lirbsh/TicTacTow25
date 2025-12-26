using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using TicTacTow25.ModelsLogic;
namespace TicTacTow25.Models
{
    public abstract class MPGameModel
    {
        protected IListenerRegistration? ilr;
        protected FbData fbd = new();
        protected abstract void OnComplete(Task task);
        protected abstract void OnChange(IDocumentSnapshot? snapshot, Exception? error);
        [Ignored]
        public EventHandler? GameChanged;
        [Ignored]
        public EventHandler? GameDeleted;
        [Ignored]
        public EventHandler? GameError;
        [Ignored]
        public string Id { get; set; } = string.Empty;
        [Ignored]
        public string MyName { get; set; } = new User().Name;
        [Ignored]
        public string MyMessage { get; set; } = string.Empty;
        [Ignored]
        public string HostName => GetPlayerName(0);
        [Ignored]
        public abstract string JoinStatus { get; }
        [Ignored]
        public int NextPlay => Players.NextPlay;
        [Ignored]
        public int MyIndex => Players.MyIndex;
        [Ignored]
        public int TotalPlayers => Players.TotalPlayers;
        [Ignored]
        public int PlayersCount => Players.Count;
        public bool IsFull { get; set; }
        public DateTime Created { get; set; }
        public int CurrentPlayers { get; set; } = 1;
        public string Message { get; set; } = Strings.Waiting;
        public Players Players { get; set; } = new();
        public abstract void RemoveSnapshotListener();
        public abstract void AddSnapshotListener();
        public abstract void JoinGame();
        public abstract void SendMessage();
        public abstract bool IsMyTurn();
        public abstract void SetDocument(Action<System.Threading.Tasks.Task> OnComplete);
        public abstract void DeleteDocument(Action<System.Threading.Tasks.Task> OnComplete);
        public abstract Position GetPlayerPosition(int playerIndex);
        public abstract Color GetPlayerColor(int playerIndex);
        public abstract string GetPlayerName(int playerIndex);
        public abstract bool IsOponnentTurn(int playerIndex);
        public abstract void Play(int rowIndex, int columnIndex);
    }
}
