using PkmWebApi.Models.Refs;
using PkmWebApi.Models.States;

namespace PkmWebApi.Dtos.Hint;
public static class HintDtoMapper
{
    public static HintDto MapToDto(HintRefModel pRef, HintStateModel pState)
    {
        return new()
        {
            Hint = pRef.Hint,
            HintType = pRef.HintType,
            ScoreCost = pRef.ScoreCost,
            IsRevealed = pState.IsRevealed
        };
    }
}
