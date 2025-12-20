namespace TicTacTow25.Models
{
    public class Position(int row, int column)
    {
        public int Row { get; set; } = row;
        public int Column { get; set; } = column;
        public Position(): this(0,0) { }
    }
}
