namespace PkmGuessGame.Models.Results
{
    public class HintResultModel
    {
        public HintResultTypes Result { get; set; }
        public string? Hint { get; set; } = string.Empty;
        public int ScoreCost { get; set; }
    }

    public enum HintResultTypes
    {
        Ok,
        AlreadyRevealed,
        AlreadyGuessed,
        Invalid
    }
}
