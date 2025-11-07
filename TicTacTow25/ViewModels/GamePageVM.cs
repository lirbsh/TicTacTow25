using CommunityToolkit.Maui.Alerts;
using TicTacTow25.Models;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.ViewModels
{
    public partial class GamePageVM : ObservableObject
    {
        private readonly Game game;
        public string MyName => game.MyName;
        public string OpponentName => game.OpponentName;
        public GamePageVM(Game game)
        {
            this.game = game;
            if(!game.IsHost)
            {
                game.GuestName = MyName;
                game.IsFull = true;
                game.SetDocument(OnComplete);
            }
        }

        private void OnComplete(Task task)
        {
            if(!task.IsCompletedSuccessfully)
                Toast.Make(Strings.JoinGameErr, CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                    
        }
    }
}
