using PkmApi.Dtos.Ability;
using PkmApi.Dtos.Move;
using PkmApi.Dtos.Pokemon;
using PkmApi.Dtos.Type;
using PkmApi.Dtos.Version;
using PkmApi.Dtos.VersionGroup;
using PkmApi.Endpoints;

namespace PkmApi
{
    public interface IPkmApi
    {
        IEndpointHandler<AbilityDto> Ability { get; }
        IEndpointHandler<MoveDto> Move { get; }
        IEndpointHandler<PkmDto> Pokemon { get; }
        IEndpointHandler<TypeDto> Type { get; }
        IEndpointHandler<VersionDto> Version { get; }
        IEndpointHandler<VersionGroupDto> VersionGroup { get; }
    }
}
