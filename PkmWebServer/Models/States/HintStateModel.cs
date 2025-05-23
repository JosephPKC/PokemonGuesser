using PkmWebServer.Models.Refs;

namespace PkmWebServer.Models.States;
public class HintStateModel
{
    public HintRefModel Ref { get; set; } = new();
    public bool IsRevealed { get; set; } = false;
}
