namespace TicTacTow25.Models
{
    public partial class IndexedButton : Button
    {
        private Color baseColor;
        public int RowIndex { get; set; } 
        public int ColumnIndex { get; set; } 
        public void RestoreColor( )
        {
            BackgroundColor = baseColor;
        }
        public IndexedButton(int rowIndex, int columnIndex, int size, Color color)
        {
            baseColor = color;
            BackgroundColor = color;
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            FontSize = 30;
            HeightRequest = size;
            WidthRequest = size;
        }
    }
}
