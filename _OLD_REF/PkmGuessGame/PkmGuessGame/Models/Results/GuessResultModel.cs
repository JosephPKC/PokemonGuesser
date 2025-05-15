namespace PkmGuessGame.Models.Results
{
    public class GuessResultModel
    {
        public GuessResultTypes Result { get; set; }
        public int ScoreChange { get; set; }
        public GuessTypes? GuessType { get; set; }
        public int? GuessId { get; set; }
        public bool IsGameDone { get; set; }
    }

    public enum GuessResultTypes
    {
        Correct,
        Old,
        Wrong,
        AlreadyGuessed
    }
}
