using PkmWebServer.Dtos.Game;
using PkmWebServer.Models.States;

namespace PkmWebServer.Controllers.Results.Hint;
public static class RevealHintResultMapper
{
    public static RevealHintResultDto GetResult(GameStateModel pState, HintResultTypes pHintResult)
    {
        return new()
        {
            NewState = GameDtoMapper.MapToDto(pState),
            Result = pHintResult
        };
    }
}
