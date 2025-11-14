using System.Diagnostics.CodeAnalysis;

namespace TicTacTow25.Models
{
    public class GameStatus
    {
        private readonly string[] msgs = [Strings.WaitMessage, Strings.PlayMessage];
        public enum Status { Wait, Play }
        public Status CurrentStatus { get; set; } = Status.Wait;
        public string StatusMessage => msgs[(int)CurrentStatus];
    }
}
