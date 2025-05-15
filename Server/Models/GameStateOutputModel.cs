namespace Server.Models
{
    public class GameStateOutputModel
    {
        public PkmModel PkmRef { get; set; } = new();
        public MoveStateModel[] Moves { get; set; } = [];
        public IEnumerable<string> WrongGuesses { get; set; } = [];
        public int Lives { get; set; } = 0;
        public bool IsDone { get; set; } = false;
        public bool IsWin { get; set; } = false;
        public StatsModel Stats { get; set; } = new();
    }
}
