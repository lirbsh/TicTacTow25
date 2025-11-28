using TicTacTow25.Models;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.ViewModels
{
    public class MPGamePageVM: ObservableObject
    {
        private readonly MPGame game;
        public string MyName => game.MyName;
        public MPGamePageVM(MPGame game)
        {
            this.game = game;
        }
        internal void AddSnapshotListener()
        {

        }

        internal void RemoveSnapshotListener()
        {

        }
    }
}
