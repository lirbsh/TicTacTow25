using CommunityToolkit.Maui.Alerts;
using Plugin.CloudFirestore;
using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class Games : GamesModel
    {
        protected override void OnGameDeleted(object? sender, EventArgs e)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Toast.Make(Strings.GameCanceld, CommunityToolkit.Maui.Core.ToastDuration.Long, 14).Show();
            });
        }
        protected override void OnComplete(Task task)
        {
            IsBusy = false;
            OnGameAdded?.Invoke(this, _currentGame!);
        }
        protected override void OnChange(IQuerySnapshot snapshot, Exception error)
        {
            fbd.GetDocumentsWhereEqualTo(Keys.GamesCollection, nameof(GameModel.IsFull), false, OnComplete);
        }

        protected override void OnComplete(IQuerySnapshot qs)
        {
            GamesList!.Clear();
            //if(qs.Documents.Count() >0)
            //{
            //    IDocumentSnapshot ds = qs.Documents.FirstOrDefault()!;
            //    Game? game = ds.ToObject<Game>();
            //}
            foreach (IDocumentSnapshot ds in qs.Documents)
            {
                Game? game = ds.ToObject<Game>();
                if (game != null)
                {
                    game.Id = ds.Id;
                    game.InitBoardAndStatus();
                    GamesList.Add(game);
                }
            }
            OnGamesChanged?.Invoke(this, EventArgs.Empty);
        }
        public override void AddGame()
        {
            IsBusy = true;
            _currentGame = new(SelectedGameSize)
            {
                IsHostUser = true
            };
            _currentGame.GameDeleted += OnGameDeleted;
            _currentGame.SetDocument(OnComplete);
        }
        public override void AddSnapshotListener()
        {
            ilr = fbd.AddSnapshotListener(Keys.GamesCollection, OnChange!);
        }
        public override void RemoveSnapshotListener()
        {
            ilr?.Remove();
        }
    }
}