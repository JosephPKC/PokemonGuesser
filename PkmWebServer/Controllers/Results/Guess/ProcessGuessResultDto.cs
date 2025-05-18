using PkmWebServer.Dtos.Game;

namespace PkmWebServer.Controllers.Results.Guess;
public class ProcessGuessResultDto
{
    public GuessResultTypes Result { get; set; }
    public GameDto NewState { get; set; } = new();
}
