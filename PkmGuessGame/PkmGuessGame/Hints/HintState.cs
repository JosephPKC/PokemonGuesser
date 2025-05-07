namespace PkmGuessGame.Hints
{
    internal class HintState
    {
        public int ScoreCost { get; set; }
        public bool IsRevealed { get; set; }
        public string Hint { get; set; } = string.Empty;
    }
}
