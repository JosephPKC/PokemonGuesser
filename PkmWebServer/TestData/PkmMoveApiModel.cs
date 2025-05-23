namespace PkmWebServer.TestData;
public class PkmMoveApiModel : BasicApiModel
{
    public NameApiModel MoveType { get; set; } = new();
    public NameApiModel DamageClass { get; set; } = new();
    public int LevelLearned { get; set; }
    public int Accuracy { get; set; }
    public int Power { get; set; }
    public int Pp { get; set; }
    public string FlavorText { get; set; } = string.Empty;
}
