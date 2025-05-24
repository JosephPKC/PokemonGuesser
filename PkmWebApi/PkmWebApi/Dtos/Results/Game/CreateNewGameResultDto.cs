using PkmWebApi.Dtos.Game;

namespace PkmWebApi.Dtos.Results.Game;
public class CreateNewGameResultDto
{
    public GameDto Game { get; set; } = new();
}
