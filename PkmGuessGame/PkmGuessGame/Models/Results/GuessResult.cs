namespace PkmGuessGame.Models.Results
{
    public class GuessResult
    {
        public GuessResultTypes Result { get; set; }
        public int ScoreGain { get; set; }
        public GuessTypes GuessType { get; set; }
        public int GuessId { get; set; }
        public int CurrentScore { get; set; }
        public bool IsGameDone { get; set; }
    }

    public enum GuessResultTypes
    {
        Correct,
        Old,
        Wrong,
        AlreadyGuessed
    }

    public enum GuessTypes
    {
        Ability,
        Move
    }
}
