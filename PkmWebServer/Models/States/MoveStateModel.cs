namespace PkmWebServer.Models.States;
public class MoveStateModel
{
    public int MoveId { get; set; }
    public bool IsAnswered { get; set; } = false;
    public int Points { get; set; }
    public HintStateModel DamageClass { get; set; } = new();
    public HintStateModel Type { get; set; } = new();
    public HintStateModel Stats { get; set; } = new();
    public HintStateModel FlavorText { get; set; } = new();
}
