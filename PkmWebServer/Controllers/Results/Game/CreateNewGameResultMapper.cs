using PkmWebServer.Dtos.Game;
using PkmWebServer.Models.States;

namespace PkmWebServer.Controllers.Results.Game;
public static class CreateNewGameResultMapper
{
    public static CreateNewGameResultDto CreateResult(GameStateModel pState)
    {
        return new()
        {
            Game = GameDtoMapper.MapToDto(pState)
        };
    }
}
