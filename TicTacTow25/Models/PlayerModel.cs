using Plugin.CloudFirestore.Attributes;
using TicTacTow25.Models;

namespace TicTacTow25.Models
{
    public abstract class PlayerModel(string name)
    {
        protected Color[] playerColors = [Colors.Magenta, Colors.Green, Colors.Orange, Colors.Beige, Colors.Red];
        public string Name { get; set; } = name;
        public Position Position { get; set; } = new(4, 2);
        public abstract Color GetColor(int index);
    }
}
