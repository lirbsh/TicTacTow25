using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class Player(string name) : PlayerModel(name)
    {
        public Player() : this(string.Empty) { }

        public override Color GetColor(int index)
        {
           return playerColors[index % playerColors.Length];
        }
    }

}
