using PkmWebApi.Dtos.Game;
using PkmWebApi.Models.States;

namespace PkmWebApi.Dtos.Results.Guess;
public static class ProcessGuessResultMapper
{
    public static ProcessGuessResultDto GetResult(GameStateModel pState, GuessResultTypes pGuessResult)
    {
        return new()
        {
            NewState = GameDtoMapper.MapToDto(pState),
            Result = pGuessResult
        };
    }
}
