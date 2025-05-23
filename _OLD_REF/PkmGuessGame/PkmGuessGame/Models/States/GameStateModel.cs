namespace PkmGuessGame.Models.States
{
    internal class GameStateModel
    {
        public StateDict Abilities { get; set; } = new();
        public StateDict Moves { get; set; } = new();
        public HashSet<string> OldMoves { get; set; } = [];

        public HashSet<string> Guesses { get; set; } = [];

        public GameStats Stats { get; set; } = new();

    }
}
