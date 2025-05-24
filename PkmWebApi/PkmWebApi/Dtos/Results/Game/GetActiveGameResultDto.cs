using PkmWebApi.Dtos.Game;

namespace PkmWebApi.Dtos.Results.Game;
public class GetActiveGameResultDto
{
    public GameDto Game { get; set; } = new();
}
