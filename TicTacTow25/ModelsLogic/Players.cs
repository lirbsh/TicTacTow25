using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class Players : PlayersModel
    {
        public override void Add(Player p)
        {
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
        public override void Play(int rowIndex, int columnIndex)
        {
            PlayersList[MyIndex].Position = new Position(rowIndex, columnIndex);
        }
    }
}
