using PkmWebApi.Models.Refs;
using PkmWebApi.Models.States;

namespace PkmWebApi.Services.GameService;
public static class MoveStateMapper
{
    public static MoveStateModel CreateState(MoveRefModel pRef)
    {
        return new()
        {
            MoveId = pRef.Id,
            IsAnswered = false,
            Points = Configs.StartingPoints,
            DamageClass = HintStateMapper.CreateState(pRef.DamageClass),
            Type = HintStateMapper.CreateState(pRef.Type),
            Stats = HintStateMapper.CreateState(pRef.Stats),
            FlavorText = HintStateMapper.CreateState(pRef.FlavorText)
        };
    }
}
