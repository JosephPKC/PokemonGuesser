using PkmWebServer.Dtos.Hint;
using PkmWebServer.Models.Refs;
using PkmWebServer.Models.States;

namespace PkmWebServer.Dtos.Guess;
public static class MoveDtoMapper
{
    public static MoveDto MapToDto(MoveRefModel pRef, MoveStateModel pState)
    {
        return new()
        {
            Id = pRef.Id,
            Name = pRef.Name,
            LevelLearned = pRef.LevelLearned,
            IsAnswered = pState.IsAnswered,
            Points = pState.Points,
            DamageClass = HintDtoMapper.MapToDto(pRef.DamageClass, pState.DamageClass),
            FlavorText = HintDtoMapper.MapToDto(pRef.FlavorText, pState.FlavorText),
            Stats = HintDtoMapper.MapToDto(pRef.Stats, pState.Stats),
            Type = HintDtoMapper.MapToDto(pRef.Type, pState.Type)
        };
    }
}
