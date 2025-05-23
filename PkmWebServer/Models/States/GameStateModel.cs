using PkmWebServer.Dtos.Game;
using PkmWebServer.Models.Refs;

namespace PkmWebServer.Models.States
{
    public class GameStateModel
    {
        public PkmRefModel Ref { get; set; } = new();
        public ISet<string> Guesses { get; set; } = new HashSet<string>();
        public ICollection<string> WrongGuesses { get; set; } = [];
        public int Lives { get; set; } = 0;
        public IDictionary<string, int> MoveNameKeys { get; set; } = new Dictionary<string, int>();
        public IDictionary<int, MoveStateModel> MoveStates { get; set; } = new Dictionary<int, MoveStateModel>();
        public GameResultTypes Result { get; set; } = GameResultTypes.Ongoing;
        public StatsModel Stats { get; set; } = new();
    }
}
