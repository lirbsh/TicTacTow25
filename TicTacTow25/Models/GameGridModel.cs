namespace TicTacTow25.Models
{
    internal abstract class GameGridModel:Grid
    {
        protected int rowSize;
        protected IndexedButton[,]? gameButtons;
        public EventHandler<IndexedButton>? OnButtonClicked;
        protected abstract void OnMyButtonClicked(object? sender, EventArgs e);
        public abstract void Init(Grid bord, int rowSize, int size, Color color);
        public abstract void UpdateButton(Position pos, Color color);
    }
}
