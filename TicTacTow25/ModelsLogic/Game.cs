using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    internal class Game:GameModel
    {
        internal Game(GameSize selectedGameSize)
        {
            HostName = new User().Name;
            RowSize = selectedGameSize.Size;
            Created = DateTime.Now;
        }
        internal Game()
        {
        }
        public override void SetDocument(Action<System.Threading.Tasks.Task> OnComplete)
        {
           Id = fbd.SetDocument(this, Keys.GamesCollection, Id, OnComplete);
        }

       
    }
}
