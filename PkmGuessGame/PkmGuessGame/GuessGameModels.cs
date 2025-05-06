namespace PkmGuessGame
{
    public class ProcessGuessResult
    {
        public GuessResults Result { get; set; }
        public int Score { get; set; }
        public int? GuessId { get; set; }
        public bool IsGameDone { get; set; }
        public int CurrentTotalScore { get; set; }
    }

    public enum GuessResults
    {
        Correct,
        OldMatch,
        Incorrect,
        Duplicate
    }

    public class RevealHintResult
    {
        public HintResults Result { get; set; }
        public string? Hint { get; set; }
    }

    public enum HintResults
    {
        Revealed,
        AlreadyRevealed,
        Missing,
        AlreadyAnswered
    }

    public class GameStats
    {
        public int NbrOfGuessesTotal { get; set; }
        public int NbrOfCorrectGuesses { get; set; }
        public int NbrOfOldCorrectGuesses { get; set; }
        public int NbrOfIncorrectGuesses { get; set; }
        public int TotalScore { get; set; }
    }

    public class PkmGameModel
    {
        public Dictionary<int, PkmMoveModel> Moves { get; set; } = [];
        public Dictionary<int, PkmOldMoveModel> OldMoves { get; set; } = [];
    }

    public class PkmMoveModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        /* Hints */
        public string Type { get; set; } = string.Empty;
        public string MoveDamageClass { get; set; } = string.Empty;
        public string FlavorText { get; set; } = string.Empty;
    }

    public class PkmOldMoveModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
