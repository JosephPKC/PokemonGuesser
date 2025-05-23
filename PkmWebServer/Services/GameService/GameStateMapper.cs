using PkmWebServer.Models.Refs;
using PkmWebServer.Models.States;

namespace PkmWebServer.Services.GameService;
public static class GameStateMapper
{
    public static GameStateModel CreateState(PkmRefModel pRef)
    {
        return new()
        {
            Ref = pRef,
            Result = Dtos.Game.GameResultTypes.Ongoing,
            Guesses = new HashSet<string>(),
            WrongGuesses = [],
            MoveNameKeys = GetMoveNameKeys(pRef),
            MoveStates = GetMoveStates(pRef),
            Lives = Configs.StartingLives,
            Stats = GetStats(pRef)
        };
    }

    private static Dictionary<string, int> GetMoveNameKeys(PkmRefModel pRef)
    {
        return pRef.Moves.ToDictionary(mKvp => mKvp.Value.NameKey, mKvp => mKvp.Key);
    }

    private static Dictionary<int, MoveStateModel> GetMoveStates(PkmRefModel pRef)
    {
        return pRef.Moves.ToDictionary(mKvp => mKvp.Key, mKvp => MoveStateMapper.CreateState(mKvp.Value));
    }

    private static StatsModel GetStats(PkmRefModel pRef)
    {
        int maxScore = pRef.Moves.Count * Configs.StartingPoints;
        return new()
        {
            PotentialScore = maxScore,
            MaxScore = maxScore
        };
    }
}
