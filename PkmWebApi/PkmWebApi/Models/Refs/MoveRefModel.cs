using PkmWebApi.Dtos.Hint;

namespace PkmWebApi.Models.Refs;
public class MoveRefModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameKey { get; set; } = string.Empty;
    public int LevelLearned { get; set; }
    public HintRefModel DamageClass { get; set; } = new() { HintType = HintTypes.DamageClass };
    public HintRefModel Type { get; set; } = new() { HintType = HintTypes.Type };
    public HintRefModel Stats { get; set; } = new() { HintType = HintTypes.Stats };
    public HintRefModel FlavorText { get; set; } = new() { HintType = HintTypes.FlavorText };
}
