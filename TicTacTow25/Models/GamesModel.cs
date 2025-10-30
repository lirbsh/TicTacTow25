using System.Collections.ObjectModel;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    internal class GamesModel
    {
        public ObservableCollection<Game>? GamesList {  get; set; }
    }
}
