using PkmWebApi.Models.Refs;

namespace PkmWebApi.Models.States;
public class HintStateModel
{
    public HintRefModel Ref { get; set; } = new();
    public bool IsRevealed { get; set; } = false;
}
