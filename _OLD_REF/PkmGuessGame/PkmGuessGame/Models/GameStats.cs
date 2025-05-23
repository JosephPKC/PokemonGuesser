namespace PkmGuessGame.Models
{
    public class GameStats
    {
        public int TotalNbrOfGuesses { get; set; } = 0;
        public int NbrCorrect { get; set; } = 0;
        public int NbrOld { get; set; } = 0;
        public int NbrWrong { get; set; } = 0;

        public int CurrentScore { get; set; } = 0; // How many points gained so far.
        public int CurrentLoss { get; set; } = 0; // How many points lost so far.
        public int PotentialScore { get; set; } = 0; // Current potential score, i.e., highest score you can achieve at this moment.
        public int MaxPotentialScore { get; set; } = 0; // Max potential score, i.e., the potential score at the start of the game.

        public double CorrectGuessRatio
        {
            get
            {
                return double.Round((double)NbrCorrect / TotalNbrOfGuesses, 2, MidpointRounding.ToZero);
            }
        }

        public double OldGuessRatio
        {
            get
            {
                return double.Round((double)NbrOld / TotalNbrOfGuesses, 2, MidpointRounding.ToZero);
            }
        }

        public double WrongGuessRatio
        {
            get
            {
                return double.Round((double)NbrWrong / TotalNbrOfGuesses, 2, MidpointRounding.ToZero);
            }
        }

        public double SuccessRatio
        {
            get
            {
                return double.Round((double)(NbrCorrect + NbrOld) / TotalNbrOfGuesses, 2, MidpointRounding.ToZero);
            }
        }

        public double PercentGained
        {
            get
            {
                return double.Round((double)CurrentScore / MaxPotentialScore, 2, MidpointRounding.ToZero);
            }
        }

        public double PercentLost
        {
            get
            {
                return double.Round((double)CurrentLoss / MaxPotentialScore, 2, MidpointRounding.ToZero);
            }
        }
    }
}
