using PkmWebApi.Dtos.Game;
using PkmWebApi.Models.States;

namespace PkmWebApi.Dtos.Results.Hint;
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
