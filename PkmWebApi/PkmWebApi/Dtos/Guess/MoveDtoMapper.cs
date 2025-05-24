using PkmWebApi.Dtos.Hint;
using PkmWebApi.Models.Refs;
using PkmWebApi.Models.States;

namespace PkmWebApi.Dtos.Guess;
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
