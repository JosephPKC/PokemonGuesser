using PkmWebServer.Dtos.Hint;

namespace PkmWebServer.Models.Refs;
public class HintRefModel
{
    public HintTypes HintType { get; set; }
    public string Hint { get; set; } = string.Empty;
    public int ScoreCost { get; set; }
}
