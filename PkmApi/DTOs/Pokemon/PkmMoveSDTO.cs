using System.Collections.Immutable;

using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    using PkmMoveVersLi = IImmutableList<PkmMoveVersSDTO>;
    public record PkmMoveSDTO(
        NamedApiResSDTO?    Move                = null,
        PkmMoveVersLi?      VersionGroupDetails = null
    );
}
