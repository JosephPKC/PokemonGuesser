using PkmWebApi.Dtos.Guess;

namespace PkmWebApi.Dtos.Game;
public class GameDto
{
    public string Name { get; set; } = string.Empty;
    public string Type1 { get; set; } = string.Empty;
    public string? Type2 { get; set; } = null;
    public IEnumerable<MoveDto> Moves { get; set; } = [];
    public IEnumerable<string> WrongGuesses { get; set; } = [];
    public int Lives { get; set; } = 0;
    public GameResultTypes Result { get; set; }
}
