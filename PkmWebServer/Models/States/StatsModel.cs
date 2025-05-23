namespace PkmWebServer.Models.States
{
    public class StatsModel
    {
        public int NbrGuesses { get; set; } = 0;
        public int NbrCorrect { get; set; } = 0;
        public int CurrentScore { get; set; } = 0;
        public int PotentialScore { get; set; } = 0;
        public int MaxScore { get; set; } = 0;
    }
}
