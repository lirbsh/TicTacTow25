using CommunityToolkit.Maui.Alerts;
using Plugin.CloudFirestore;
using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class MPGames:MPGamesModel
    {
        public override void AddSnapshotListener()
        {
            ilr = fbd.AddSnapshotListener(Keys.MPGamesCollection, OnChange!);
        }
        public override void RemoveSnapshotListener()
        {
            ilr?.Remove();
        }
        public override void AddGame()
        {
            IsBusy = true;
            _currentGame = new(SelectedTotalPlayers);
            
            _currentGame.OnGameDeleted += OnGameDeleted;
            _currentGame.SetDocument(OnComplete);
        }
        private void OnGameDeleted(object? sender, EventArgs e)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Toast.Make(Strings.GameCanceld, CommunityToolkit.Maui.Core.ToastDuration.Long, 14).Show();
            });
        }
        private void OnComplete(Task task)
        {
            IsBusy = false;
            if (task.IsCompletedSuccessfully)
                OnGameAdded?.Invoke(this, _currentGame!);
            else if (task.IsFaulted && task.Exception != null)
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Toast.Make(fbd.GetErrorMessage(task.Exception.Message), CommunityToolkit.Maui.Core.ToastDuration.Long, 14).Show();
                });
            }
        }
        private void OnChange(IQuerySnapshot snapshot, Exception error)
        {
            fbd.GetDocumentsWhereEqualTo(Keys.MPGamesCollection, nameof(MPGameModel.IsFull), false, OnComplete);
        }

        private void OnComplete(IQuerySnapshot qs)
        {
            GamesList!.Clear();
            //if(qs.Documents.Count() >0)
            //{
            //    IDocumentSnapshot ds = qs.Documents.FirstOrDefault()!;
            //    Game? game = ds.ToObject<Game>();
            //}
            foreach (IDocumentSnapshot ds in qs.Documents)
            {
                MPGame? game = ds.ToObject<MPGame>();
                if (game != null)
                {
                    game.Id = ds.Id;
                    GamesList.Add(game);
                }
            }
            OnGamesChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}
