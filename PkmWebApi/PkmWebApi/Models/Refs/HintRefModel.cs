using PkmWebApi.Dtos.Hint;

namespace PkmWebApi.Models.Refs;
public class HintRefModel
{
    public HintTypes HintType { get; set; }
    public string Hint { get; set; } = string.Empty;
    public int ScoreCost { get; set; }
}
