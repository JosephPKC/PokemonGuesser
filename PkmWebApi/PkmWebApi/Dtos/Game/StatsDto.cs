namespace PkmWebApi.Dtos.Stats;
public class StatsDto
{
    public int NbrGuesses { get; set; }
    public int NbrCorrect { get; set; }
    public int NbrIncorrect { get; set; }
    public double CorrectRatio { get; set; }
    public double IncorrectRatio { get; set; }
    public double GuessRatio { get; set; } // Correct / Incorrect

    public int CurrentScore { get; set; }
    public int PotentialScore { get; set; }
    public int LostScore { get; set; }
    public int MaxScore { get; set; }
    public double ScoreProgressRatio { get; set; } // CurrentScore / PotentialScore
    public double PotentialScoreRatio { get; set; } // PotentialScore / MaxScore
    public double LostScoreRatio { get; set; }  // LostScore / MaxScore
    public double TotalScoreRatio { get; set; } // CurrentScore / MaxScore
}
