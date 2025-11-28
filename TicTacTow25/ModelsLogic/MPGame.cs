using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class MPGame:MPGameModel
    {
        public MPGame(int totalPlayers)
        {
            TotalPlayers = totalPlayers;
            HostName = new User().Name;
        }

        public override string JoinStatus => CurrentPlayers + "/" + TotalPlayers;

        public override void SetDocument(Action<Task> OnComplete)
        {
            Id = fbd.SetDocument(this, Keys.MPGamesCollection, Id, OnComplete);
        }
    }
}
