namespace TicTacTow25.Models
{
    internal abstract class GameGridModel:Grid
    {
        protected int rowSize;
        protected IndexedButton[,]? gameButtons;
        public EventHandler<IndexedButton>? ButtonClicked;
        protected abstract void OnButtonClicked(object? sender, EventArgs e);
        public abstract void Init(Grid bord, int rowSize, int size, Color color);
        public abstract void UpdateButton(Position pos, Color color);
        public abstract void RestoreColors();
    }
}
