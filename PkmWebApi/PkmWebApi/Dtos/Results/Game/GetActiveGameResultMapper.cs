using PkmWebApi.Dtos.Game;
using PkmWebApi.Models.States;

namespace PkmWebApi.Dtos.Results.Game;
public static class GetActiveGameResultMapper
{
    public static GetActiveGameResultDto CreateResult(GameStateModel pState)
    {
        return new()
        {
            Game = GameDtoMapper.MapToDto(pState)
        };
    }
}
