using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    public abstract class OponnentsGridModel(Grid grdOponnents, MPGame game)
    {
        protected Grid grdOponnents = grdOponnents;
        protected MPGame game = game;
        protected readonly List<Label> lstOponnentsLabels = [];
        public abstract void DisplayOponnentsNames();
    }
}
