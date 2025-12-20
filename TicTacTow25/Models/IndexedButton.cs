namespace TicTacTow25.Models
{
    public partial class IndexedButton : Button
    {
        public int RowIndex { get; set; } 
        public int ColumnIndex { get; set; } 
        public IndexedButton(int rowIndex, int columnIndex, int size, Color color)
        {
            BackgroundColor = color;
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            FontSize = 30;
            HeightRequest = size;
            WidthRequest = size;
        }
    }
}
