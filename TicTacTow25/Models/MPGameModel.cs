using Plugin.CloudFirestore.Attributes;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    public abstract class MPGameModel
    {
        protected FbData fbd = new();

        [Ignored]
        public EventHandler? OnGameDeleted;
        [Ignored]
        public string Id { get; set; } = string.Empty;
        [Ignored]
        public string MyName { get; set; } = new User().Name;
        public string HostName { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public int TotalPlayers { get; set; }
        public int CurrentPlayers { get; set; } = 1;
        public bool IsFull { get; set; }
        [Ignored]
        public abstract string JoinStatus { get; } 

        public abstract void SetDocument(Action<System.Threading.Tasks.Task> OnComplete);

    }
}
