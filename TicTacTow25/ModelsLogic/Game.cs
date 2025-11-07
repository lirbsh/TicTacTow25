using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class Game:GameModel
    {
        public override string OpponentName => IsHost ? GuestName : HostName;

        public Game(GameSize selectedGameSize)
        {
            HostName = new User().Name;
            RowSize = selectedGameSize.Size;
            Created = DateTime.Now;
        }
        public Game()
        {
        }
        public override void SetDocument(Action<System.Threading.Tasks.Task> OnComplete)
        {
           Id = fbd.SetDocument(this, Keys.GamesCollection, Id, OnComplete);
        }

       
    }
}
