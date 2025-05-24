using PkmWebApi.Models.Refs;
using PkmWebApi.Models.States;

namespace PkmWebApi.Services.GameService;
public static class HintStateMapper
{
    public static HintStateModel CreateState(HintRefModel pRef)
    {
        return new()
        {
            Ref = pRef,
            IsRevealed = false
        };
    }
}
