using PkmWebServer.Models.Refs;
using PkmWebServer.Models.States;

namespace PkmWebServer.Services.GameService;
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
