namespace TicTacTow25.Models
{
    public class DisplayMoveArgs(int rowIndex, int columnIndex, string symbol) : EventArgs
    {
        public int RowIndex { get; set; } = rowIndex;
        public int ColumnIndex { get; set; } = columnIndex;
        public string Symbol { get; set; } = symbol;
    }
}
