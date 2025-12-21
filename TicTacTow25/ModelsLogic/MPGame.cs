using Plugin.CloudFirestore;
using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class MPGame:MPGameModel
    {
        public override string JoinStatus => CurrentPlayers + "/" + Players.TotalPlayers;
        public MPGame(int totalPlayers)
        {
            
            Created = DateTime.Now;
            Player p = new(new User().Name,0);
            Players.Add(p);
            Players.TotalPlayers = totalPlayers;
            Players.NextPlay = totalPlayers - 1;
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
                int myIndex = Players.MyIndex;
                Players = game.Players;
                Players.MyIndex = myIndex;
                Message = game.Message;
                if(CurrentPlayers == Players.Count)
                    OnGameChanged?.Invoke(this, EventArgs.Empty);
                else
                    OnGameError?.Invoke(this, EventArgs.Empty);
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
            if (CurrentPlayers + 1 == Players.TotalPlayers)
                fbd.UpdateField(Keys.MPGamesCollection, Id, nameof(IsFull), true, OnComplete);
            Players.MyIndex = CurrentPlayers;
            Player p = new(MyName, CurrentPlayers);
            Players.Add(p);
            fbd.StartBatch();
            fbd.BatchIncrementField(Keys.MPGamesCollection, Id, nameof(CurrentPlayers), 1);
            fbd.BatchUpdateField(Keys.MPGamesCollection, Id, nameof(Players), Players);
            fbd.CommitBatch(OnComplete);
        }
        public override void SendMessage()
        {
            Players.SetNextPlayer();
            Dictionary<string, object> dict = new()
            {
                { nameof(Players), Players },
                { nameof(Message), MyMessage }
            };
            fbd.UpdateFields(Keys.MPGamesCollection, Id, dict, OnComplete);
        }
        public override bool IsMyTurn()
        {
            return Players.IsMyTurn();
        }
        public override Position GetPlayerPosition(int playerIndex)
        {
            return Players.PlayersList[playerIndex].Position;
        }
        public override Color GetPlayerColor(int playerIndex)
        {
            return Players.PlayersList[playerIndex].Color;
        }

        public override string GetPlayerName(int playerIndex)
        {
            return Players.PlayersList[playerIndex].Name;
        }

        public override bool IsOponnentTurn(int playerIndex)
        {
           return Players.IsOponnentTurn(playerIndex);
        }
    }
}