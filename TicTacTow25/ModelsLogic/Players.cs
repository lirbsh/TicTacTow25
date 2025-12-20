using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class Players : PlayersModel
    {
        public override void Add(Player p)
        {
            p.Position = startPositions[MyIndex];
            PlayersList.Add(p);
        }

        public override string GetPlayerName(int index)
        {
            return PlayersList[index].Name;
        }

        public override bool IsMyTurn()
        {
            return NextPlay == MyIndex;
        }

        public override void SetNextPlayer()
        {
            NextPlay = (NextPlay + 1) % TotalPlayers;
        }
        public override bool IsOponnentTurn(int oponnentIndex)
        {
            return oponnentIndex == NextPlay;
        }
    }
}
