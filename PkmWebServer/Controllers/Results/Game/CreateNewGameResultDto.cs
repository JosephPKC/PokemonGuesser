using PkmWebServer.Dtos.Game;

namespace PkmWebServer.Controllers.Results.Game;
public class CreateNewGameResultDto
{
    public GameDto Game { get; set; } = new();
}
