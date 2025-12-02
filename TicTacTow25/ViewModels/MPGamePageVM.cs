using TicTacTow25.Models;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.ViewModels
{
    public class MPGamePageVM: ObservableObject
    {
        private readonly MPGame game;
        private readonly List<Label> lstOponnentsLabels = new();
        public string MyName => game.MyName;
        public MPGamePageVM(MPGame game,Grid grdOponnents)
        {
            this.game = game;
            InitOponnentsGrid(grdOponnents);
        }

        private void InitOponnentsGrid(Grid grdOponnents)
        {
            int oponnentsCount = game.TotalPlayers - 1;
            for (int i = 0; i < oponnentsCount; i++)
            {
                grdOponnents.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                lstOponnentsLabels.Add(new Label
                {
                    Text = Strings.Waiting,
                    FontSize = 16,
                    Margin = new Thickness(5)
                });
                grdOponnents.Add(lstOponnentsLabels[i], i, 0);
            }
        }

        internal void AddSnapshotListener()
        {
            game.AddSnapshotListener();
        }

        internal void RemoveSnapshotListener()
        {
            game.RemoveSnapshotListener();
        }
    }
}
