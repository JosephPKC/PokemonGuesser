namespace PkmWebApi.Dtos.Hint;
public class HintDto
{
    public HintTypes HintType { get; set; }
    public string Hint { get; set; } = string.Empty;
    public int ScoreCost { get; set; }
    public bool IsRevealed { get; set; } = false;
}
