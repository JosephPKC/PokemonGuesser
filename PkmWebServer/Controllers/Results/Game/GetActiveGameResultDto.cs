using PkmWebServer.Dtos.Game;

namespace PkmWebServer.Controllers.Results.Game;
public class GetActiveGameResultDto
{
    public GameDto Game { get; set; } = new();
}
