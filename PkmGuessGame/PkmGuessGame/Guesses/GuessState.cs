using PkmGuessGame.Hints;

namespace PkmGuessGame.Guesses
{
    internal class GuessState
    {
        public int CurrentScore { get; set; }
        public bool IsAnswered { get; set; }
        public List<HintState> Hints { get; set; } = [];
    }
}
