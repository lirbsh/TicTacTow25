namespace TicTacTow25.Models
{
    internal class GameModel
    {
        public string HostName { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public int RowSize {  get; set; }
    }
}
