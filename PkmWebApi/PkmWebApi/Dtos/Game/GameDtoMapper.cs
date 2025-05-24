using PkmWebApi.Dtos.Guess;
using PkmWebApi.Models.States;

namespace PkmWebApi.Dtos.Game;
public static class GameDtoMapper
{
    public static GameDto MapToDto(GameStateModel pState)
    {
        return new()
        {
            Name = pState.Ref.Name,
            Type1 = pState.Ref.Type1,
            Type2 = pState.Ref.Type2,
            Lives = pState.Lives,
            WrongGuesses = pState.WrongGuesses,
            Result = pState.Result,
            Moves = MapMoves(pState)
        };
    }

    private static List<MoveDto> MapMoves(GameStateModel pState)
    {
        List<MoveDto> moves = [.. pState.MoveStates.Select((kvp) => MoveDtoMapper.MapToDto(pState.Ref.Moves[kvp.Key], kvp.Value))];
        moves.Sort((x, y) => x.LevelLearned.CompareTo(y.LevelLearned));
        return moves;
    }
}
