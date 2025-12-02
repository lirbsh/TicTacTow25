using Plugin.CloudFirestore;
using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class MPGame:MPGameModel
    {
        public MPGame(int totalPlayers)
        {
            TotalPlayers = totalPlayers;
            Created = DateTime.Now;
            PlayersNames.Add(new User().Name);
        }
        public MPGame() { }

        public override string JoinStatus => CurrentPlayers + "/" + TotalPlayers;

        public override void SetDocument(Action<Task> OnComplete)
        {
            Id = fbd.SetDocument(this, Keys.MPGamesCollection, Id, OnComplete);
        }
        public override void AddSnapshotListener()
        {
            ilr = fbd.AddSnapshotListener(Keys.GamesCollection, Id, OnChange);
        }

        private void OnChange(IDocumentSnapshot? snapshot, Exception? error)
        {
            
        }

        public override void RemoveSnapshotListener()
        {
            ilr?.Remove();
            DeleteDocument(OnComplete);
        }

        private void OnComplete(Task task)
        {
            
        }

        public override void DeleteDocument(Action<Task> OnComplete)
        {
            fbd.DeleteDocument(Keys.MPGamesCollection, Id, OnComplete);

        }

        public override void JoinGame()
        {
            if (CurrentPlayers + 1 == TotalPlayers)
                fbd.UpdateField(Keys.MPGamesCollection, Id, nameof(IsFull), true, OnComplete);
            PlayersNames.Add(MyName);
            fbd.StartBatch();
            fbd.BatchIncrementField(Keys.MPGamesCollection, Id, nameof(CurrentPlayers), 1);
            fbd.BatchUpdateField(Keys.MPGamesCollection, Id, nameof(PlayersNames), PlayersNames);
            fbd.CommitBatch(OnComplete);
        }
    }
}
