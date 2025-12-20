using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    public abstract class MPGameModel
    {
        protected readonly Color[] playerColors = [ Colors.Cyan, Colors.Magenta, Colors.Green, Colors.Orange ];
        protected IListenerRegistration? ilr;

        protected FbData fbd = new();
        protected abstract void OnComplete(Task task);
        protected abstract void OnChange(IDocumentSnapshot? snapshot, Exception? error);

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
        public string HostName => Players.GetPlayerName(0);
        [Ignored]
        public abstract string JoinStatus { get; }
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
        public abstract Color GetPlayerColor(int playerIndex);
    }
}
