namespace PkmGuessGame.Models.States
{
    internal class GuessStateModel
    {
        public int CurrentScore { get; set; }
        public bool IsAnswered { get; set; }
        public Dictionary<HintTypes, HintStateModel> Hints { get; set; } = [];
    }
}
