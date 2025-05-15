namespace PkmGuessGame.Models.States
{
    internal class HintStateModel
    {
        public int ScoreCost { get; set; }
        public bool IsRevealed { get; set; }
        public string Hint { get; set; } = string.Empty;
    }
}
