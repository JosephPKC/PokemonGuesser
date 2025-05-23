using PkmWebServer.Dtos.Game;
using PkmWebServer.Models.States;

namespace PkmWebServer.Controllers.Results.Guess;
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
