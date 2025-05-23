using PkmWebServer.Dtos.Hint;

namespace PkmWebServer.Dtos.Guess;
public class MoveDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int LevelLearned { get; set; }
    public bool IsAnswered { get; set; } = false;
    public int Points { get; set; }
    public HintDto DamageClass { get; set; } = new();
    public HintDto Type { get; set; } = new();
    public HintDto Stats { get; set; } = new();
    public HintDto FlavorText { get; set; } = new();
}
