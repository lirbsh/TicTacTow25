namespace TicTacTow25.Models
{
    public class IndexedButton:Button
    {
        public int RowIndex { get; set; } 
        public int ColumnIndex { get; set; } 
        public IndexedButton(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            FontSize = 40;
            HeightRequest = 70;
            WidthRequest = HeightRequest;
        }
    }
}
