using PkmWebServer.Dtos.Game;
using PkmWebServer.Models.States;

namespace PkmWebServer.Controllers.Results.Game;
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
