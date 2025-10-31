using CommunityToolkit.Maui.Alerts;
using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    internal class Games : GamesModel
    {
        internal void AddGame(GameSize selectedGameSize)
        {
            IsBusy = true;
            Game game= new Game(selectedGameSize);
            game.SetDocument(OnComplete);
        }
        private void OnComplete(Task task)
        {
            IsBusy = false;
            OnGameAdded?.Invoke(this, task.IsCompletedSuccessfully);
        }
    }
}
