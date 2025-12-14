namespace TicTacTow25.Models
{
    internal abstract class GameGridModel:Grid
    {
        protected int rowSize;
        protected IndexedButton[,]? gameButtons;
        protected abstract void OnMyButtonClicked(object? sender, EventArgs e);
        public abstract void Init(Grid bord, int rowSize);
        public EventHandler<IndexedButton>? OnButtonClicked;


    }
}
