using PkmWebApi.Dtos.Stats;
using PkmWebApi.Models.States;

namespace PkmWebApi.Dtos.Game;
public static class StatsDtoMapper
{
    public static StatsDto MapToDto(StatsModel pModel)
    {
        StatsDto stats = new()
        {
            NbrCorrect = pModel.NbrCorrect,
            NbrGuesses = pModel.NbrGuesses,
            CurrentScore = pModel.CurrentScore,
            PotentialScore = pModel.PotentialScore,
            MaxScore = pModel.MaxScore
        };

        stats.NbrIncorrect = stats.NbrGuesses - stats.NbrCorrect;
        stats.CorrectRatio = GetRatio(stats.NbrCorrect, stats.NbrGuesses);
        stats.IncorrectRatio = GetRatio(stats.NbrIncorrect, stats.NbrGuesses);
        stats.GuessRatio = GetRatio(stats.NbrCorrect, stats.NbrIncorrect);

        stats.LostScore = stats.MaxScore - stats.PotentialScore;
        stats.ScoreProgressRatio = GetRatio(stats.CurrentScore, stats.PotentialScore);
        stats.PotentialScoreRatio = GetRatio(stats.PotentialScore, stats.MaxScore);
        stats.LostScoreRatio = GetRatio(stats.LostScore, stats.MaxScore);
        stats.TotalScoreRatio = GetRatio(stats.CurrentScore, stats.MaxScore);

        return stats;
    }

    private static double GetRatio(int pNom, int pDenom)
    {
        if (pNom == 0 || pDenom == 0)
        {
            return 0;
        }
        return (double)pNom / pDenom * 100;
    }
}
