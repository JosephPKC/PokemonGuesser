using PkmWebApi.Dtos.Game;
using PkmWebApi.Models.States;

namespace PkmWebApi.Dtos.Results.Game;
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
