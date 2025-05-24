namespace PkmWebApi.Models.Refs;
public class PkmRefModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type1 { get; set; } = string.Empty;
    public string? Type2 { get; set; } = null;
    public IDictionary<int, MoveRefModel> Moves { get; set; } = new Dictionary<int, MoveRefModel>();
}
