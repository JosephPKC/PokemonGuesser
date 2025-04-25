using PkmApi.Dtos;
using PkmApi.Dtos.Game.Generation;
using PkmApi.Dtos.Game.Pokedex;
using PkmApi.Dtos.Game.VersionGroup;
using PkmApi.Dtos.Move.Move;
using PkmApi.Dtos.Move.MoveDamageClass;
using PkmApi.Dtos.Move.MoveLearnMethod;
using PkmApi.Dtos.Pokemon.Ability;
using PkmApi.Dtos.Pokemon.Form;
using PkmApi.Dtos.Pokemon.Pokemon;
using PkmApi.Dtos.Pokemon.Species;
using PkmApi.Dtos.Pokemon.Type;

namespace PkmDataRetrieval.Test.Fakes.TestEndpointHandler
{
    internal static class TestDtos
    {
        public static TDto? Get<TDto>() where TDto : class, IPkmApiDto
        {
            return typeof(TDto).Name switch
            {
                nameof(AbilityDto) => new AbilityDto(1, "ability") as TDto,
                nameof(FormDto) => new FormDto(1, "form") as TDto,
                nameof(GenerationDto) => new GenerationDto(1, "gen") as TDto,
                nameof(MoveDto) => new MoveDto(1, "move") as TDto,
                nameof(MoveDamageClassDto) => new MoveDamageClassDto(1, "move dc") as TDto,
                nameof(MoveLearnMethodDto) => new MoveLearnMethodDto(1, "move lm") as TDto,
                nameof(PokedexDto) => new PokedexDto(1, "pokedex") as TDto,
                nameof(PkmDto) => new PkmDto(1, "pkm") as TDto,
                nameof(SpeciesDto) => new SpeciesDto(1, "species") as TDto,
                nameof(TypeDto) => new TypeDto(1, "type") as TDto,
                nameof(VersionGroupDto) => new VersionGroupDto(1, "version group") as TDto,
                _ => null
            };
        }
    }
}
