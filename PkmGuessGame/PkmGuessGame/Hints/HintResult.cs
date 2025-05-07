namespace PkmGuessGame.Hints
{
    public class HintResult
    {
        public HintResultTypes Result { get; set; }
        public string Hint { get; set; } = string.Empty;
        public int ScoreCost { get; set; }
        public int PotentialScore { get; set; }
    }

    public enum HintResultTypes
    {
        Ok,
        AlreadyRevealed,
        AlreadyGuessed
    }
}
