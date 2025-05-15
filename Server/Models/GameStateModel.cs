namespace Server.Models
{
    public class GameStateModel
    {
        public PkmModel PkmRef { get; set; } = new();
        public ISet<string> Guesses { get; set; } = new HashSet<string>();
        public ICollection<string> WrongGuesses { get; set; } = [];
        public int Lives { get; set; } = 0;
        public IDictionary<string, int> MoveNameKey { get; set; } = new Dictionary<string, int>();
        public IDictionary<int, MoveStateModel> MoveStates { get; set; } = new Dictionary<int, MoveStateModel>();
        public bool IsDone { get; set; } = false;
        public bool IsWin { get; set; } = false;
        public StatsModel Stats { get; set; } = new();

        public GameStateOutputModel ToOutput()
        {
            GameStateOutputModel state = new()
            {
                PkmRef = PkmRef,
                IsDone = IsDone,
                Moves = MoveStates.Values.ToArray(),
                Stats = Stats,
                WrongGuesses = WrongGuesses,
                Lives = Lives,
                IsWin = IsWin
            };

            List<MoveStateModel> moves = state.Moves.ToList();
            moves.Sort((x, y) => x.MoveRef.LevelLearned.CompareTo(y.MoveRef.LevelLearned));
            state.Moves = moves.ToArray();

            return state;
        }
    }
}
