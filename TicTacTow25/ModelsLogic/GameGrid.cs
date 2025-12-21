using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    internal partial class GameGrid : GameGridModel
    {
        public override void Init(Grid board, int rowSize, int size, Color color)
        {
            this.Parent = board;
            this.rowSize = rowSize;
            gameButtons = new IndexedButton[rowSize, rowSize];
            IndexedButton btn;
            for (int i = 0; i < rowSize; i++)
            {
                RowDefinitions.Add(new RowDefinition {  Height = GridLength.Auto });
                ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }
            for (int i = 0; i < rowSize; i++)
                for (int j = 0; j < rowSize; j++)
                {
                    btn = new IndexedButton(i, j, size,color);
                    gameButtons[i, j] = btn;
                    btn.Clicked += OnButtonClicked;
                    ((Grid)this.Parent).Add(btn, j, i);
                }
        }

        public override void RestoreColors()
        {
            foreach (IndexedButton btn in gameButtons!)
                btn.RestoreColor();
        }

        public override void UpdateButton(Position pos, Color color)
        {
            gameButtons![pos.Row, pos.Column].BackgroundColor = color;
        }

        protected override void OnButtonClicked(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, (IndexedButton)sender! );
        }
        internal void UpdateDisplay(DisplayMoveArgs e)
        {
            gameButtons![e.RowIndex, e.ColumnIndex].Text = e.Symbol;
        }
    }
}
