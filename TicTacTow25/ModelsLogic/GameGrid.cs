using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    internal partial class GameGrid : GameGridModel
    {
        public override void Init(Grid board, int rowSize)
        {
            this.Parent = board;
            this.rowSize = rowSize;
            gameButtons = new IndexedButton[rowSize, rowSize];
            IndexedButton btn;
            for (int i = 0; i < rowSize; i++)
            {
                RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }
            for (int i = 0; i < rowSize; i++)
                for (int j = 0; j < rowSize; j++)
                {
                    btn = new IndexedButton(i, j);
                    gameButtons[i, j] = btn;
                    btn.Clicked += OnMyButtonClicked;
                    ((Grid)this.Parent).Add(btn, j, i);
                }
        }
        protected override void OnMyButtonClicked(object? sender, EventArgs e)
        {
            OnButtonClicked?.Invoke(this, (IndexedButton)sender! );
        }
        internal void UpdateDisplay(DisplayMoveArgs e)
        {
            gameButtons![e.RowIndex, e.ColumnIndex].Text = e.Symbol;
        }
    }
}
