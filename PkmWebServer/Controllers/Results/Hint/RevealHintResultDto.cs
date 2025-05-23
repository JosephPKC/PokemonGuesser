using PkmWebServer.Dtos.Game;

namespace PkmWebServer.Controllers.Results.Hint;
public class RevealHintResultDto
{
    public HintResultTypes Result { get; set; }
    public GameDto NewState { get; set; } = new();
}
