using Plugin.CloudFirestore;
using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class MPGame:MPGameModel
    {
        public override string JoinStatus => CurrentPlayers + "/" + TotalPlayers;
        public MPGame(int totalPlayers)
        {
            TotalPlayers = totalPlayers;
            NextPlay = totalPlayers - 1;
            Created = DateTime.Now;
            PlayersNames.Add(new User().Name);
        }
        public MPGame() { }
        protected override void OnComplete(Task task)
        {
            //code for handeling completion of tasks if needed
        }
        protected override void OnChange(IDocumentSnapshot? snapshot, Exception? error)
        {
            MPGame? game = snapshot?.ToObject<MPGame>();
            if (game != null)
            {
                CurrentPlayers = game.CurrentPlayers;
                PlayersNames = game.PlayersNames;
                Message = game.Message;
                NextPlay = game.NextPlay;
                OnGameChanged?.Invoke(this, EventArgs.Empty);
            }
            else
                OnGameDeleted?.Invoke(this, EventArgs.Empty);
        }
        public override void SetDocument(Action<Task> OnComplete)
        {
            Id = fbd.SetDocument(this, Keys.MPGamesCollection, Id, OnComplete);
        }
        public override void AddSnapshotListener()
        {
            ilr = fbd.AddSnapshotListener(Keys.MPGamesCollection, Id, OnChange);
        }
        public override void RemoveSnapshotListener()
        {
            ilr?.Remove();
            DeleteDocument(OnComplete);
        }
        public override void DeleteDocument(Action<Task> OnComplete)
        {
            fbd.DeleteDocument(Keys.MPGamesCollection, Id, OnComplete);
        }

        public override void JoinGame()
        {
            if (CurrentPlayers + 1 == TotalPlayers)
                fbd.UpdateField(Keys.MPGamesCollection, Id, nameof(IsFull), true, OnComplete);
            MyIndex = CurrentPlayers;
            PlayersNames.Add(MyName);
            fbd.StartBatch();
            fbd.BatchIncrementField(Keys.MPGamesCollection, Id, nameof(CurrentPlayers), 1);
            fbd.BatchUpdateField(Keys.MPGamesCollection, Id, nameof(PlayersNames), PlayersNames);
            fbd.CommitBatch(OnComplete);
        }
        public override void SendMessage()
        {
            NextPlay = (NextPlay + 1) % TotalPlayers;
            Dictionary<string, object> dict = new()
            {
                { nameof(NextPlay), NextPlay },
                { nameof(Message), MyMessage }
            };
            fbd.UpdateFields(Keys.MPGamesCollection, Id, dict, OnComplete);
        }
        public override bool IsMyTurn()
        {
            return NextPlay == MyIndex;
        }
        public override bool IsOponnentTurn(int oponnentIndex)
        {
            return oponnentIndex == NextPlay;
        }
    }
}