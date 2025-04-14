using PkmApi.Dtos.Game.Generation;
using PkmApi.Dtos.Game.Pokedex;
using PkmApi.Dtos.Game.Version;
using PkmApi.Dtos.Game.VersionGroup;
using PkmApi.Dtos.Move.Move;
using PkmApi.Dtos.Move.MoveDamageClass;
using PkmApi.Dtos.Move.MoveLearnMethod;
using PkmApi.Dtos.Pokemon.Ability;
using PkmApi.Dtos.Pokemon.Form;
using PkmApi.Dtos.Pokemon.Pokemon;
using PkmApi.Dtos.Pokemon.Species;
using PkmApi.Dtos.Pokemon.Type;
using PkmApi.Endpoints;

namespace PkmApi
{
    public interface IPkmApi
    {
        IEndpointHandler<AbilityDto> Ability { get; }
        IEndpointHandler<FormDto> Form { get; }
        IEndpointHandler<GenerationDto> Generation { get; }
        IEndpointHandler<MoveDto> Move { get; }
        IEndpointHandler<MoveDamageClassDto> MoveDamageClass { get; }
        IEndpointHandler<MoveLearnMethodDto> MoveLearnMethod { get; }
        IEndpointHandler<PokedexDto> Pokedex { get; }
        IEndpointHandler<PkmDto> Pokemon { get; }
        IEndpointHandler<SpeciesDto> Species { get; }
        IEndpointHandler<TypeDto> Type { get; }
        IEndpointHandler<VersionDto> Version { get; }
        IEndpointHandler<VersionGroupDto> VersionGroup { get; }
    }
}
