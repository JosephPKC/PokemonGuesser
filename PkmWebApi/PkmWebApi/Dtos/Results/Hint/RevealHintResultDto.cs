using PkmWebApi.Dtos.Game;

namespace PkmWebApi.Dtos.Results.Hint;
public class RevealHintResultDto
{
    public HintResultTypes Result { get; set; }
    public GameDto NewState { get; set; } = new();
}
