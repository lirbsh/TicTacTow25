using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    public abstract class MPGameModel
    {
        protected IListenerRegistration? ilr;

        protected FbData fbd = new();
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
        public DateTime Created { get; set; }
        public int TotalPlayers { get; set; }
        public int CurrentPlayers { get; set; } = 1;
        public List<string> PlayersNames { get; set; } = [];
        [Ignored]
        public string HostName => PlayersNames[0];
        public bool IsFull { get; set; }
        [Ignored]
        public abstract string JoinStatus { get; }

        public abstract void RemoveSnapshotListener();
        public abstract void AddSnapshotListener();
        public abstract void SetDocument(Action<System.Threading.Tasks.Task> OnComplete);
        public abstract void DeleteDocument(Action<System.Threading.Tasks.Task> OnComplete);
        public abstract void JoinGame();
    }
}
