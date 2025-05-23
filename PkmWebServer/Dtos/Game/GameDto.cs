using PkmWebServer.Dtos.Guess;

namespace PkmWebServer.Dtos.Game;
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
