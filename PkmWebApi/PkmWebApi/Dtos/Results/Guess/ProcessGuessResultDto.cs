using PkmWebApi.Dtos.Game;

namespace PkmWebApi.Dtos.Results.Guess;
public class ProcessGuessResultDto
{
    public GuessResultTypes Result { get; set; }
    public GameDto NewState { get; set; } = new();
}
