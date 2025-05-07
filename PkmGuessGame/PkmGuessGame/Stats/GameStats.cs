namespace PkmGuessGame.Stats
{
    public class GameStats
    {
        public int TotalNbrOfGuesses { get; set; }
        public int NbrCorrect { get; set; }
        public int NbrOld { get; set; }
        public int NbrWrong { get; set; }

        public int CurrentScore { get; set; }
        public int CurrentLoss { get; set; }
        public int PotentialScore { get; set; }
        public int MaxPotentialScore { get; set; }
    }
}
