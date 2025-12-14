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
        public int MyIndex { get; protected set; } = 0;
        [Ignored]
        public EventHandler? OnGameChanged;
        [Ignored]
        public EventHandler? OnGameDeleted;
        [Ignored]
        public string Id { get; set; } = string.Empty;
        [Ignored]
        public string MyName { get; set; } = new User().Name;
        [Ignored]
        public string MyMessage { get; set; } = string.Empty;
        [Ignored]
        public string HostName => PlayersNames[0];
        public bool IsFull { get; set; }
        [Ignored]
        public DateTime Created { get; set; }
        public int TotalPlayers { get; set; }
        public int CurrentPlayers { get; set; } = 1;
        public int NextPlay { get; set; } 
        public string Message { get; set; } = Strings.Waiting;
        public List<string> PlayersNames { get; set; } = [];
        public abstract string JoinStatus { get; }
        public abstract void RemoveSnapshotListener();
        public abstract void AddSnapshotListener();
        public abstract void JoinGame();
        public abstract void SendMessage();
        public abstract bool IsMyTurn();
        public abstract bool IsOponnentTurn(int oponnentIndex);
        public abstract void SetDocument(Action<System.Threading.Tasks.Task> OnComplete);
        public abstract void DeleteDocument(Action<System.Threading.Tasks.Task> OnComplete);
    }
}
