using Plugin.CloudFirestore.Attributes;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    public abstract class PlayersModel
    {
        protected Position[] startPositions = [new(4, 2), new(2, 4), new(0, 2), new(2, 0), new(2, 2)];
        [Ignored]
        public int MyIndex { get; set; } = 0;
        [Ignored]
        public int Count => PlayersList.Count; 
        public List<Player> PlayersList { get; set; } = [];
        public int NextPlay { get; set; }
        public int TotalPlayers { get; set; }

        public abstract void Add(Player p);
        public abstract string GetPlayerName(int index);
        public abstract bool IsOponnentTurn(int oponnentIndex);
        public abstract void SetNextPlayer();
        public abstract bool IsMyTurn();
       
    }
}
