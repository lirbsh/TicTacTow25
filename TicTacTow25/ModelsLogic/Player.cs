using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class Player(string name,int index) : PlayerModel(name, index)
    {
        public Player() : this(string.Empty, 0) { }
    }
}
