using System.Collections.Immutable;

using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    using PkmHeldItemVersLi = IImmutableList<PkmHeldItemVersSDTO>;

    public record PkmHeldItemSDTO(
        NamedApiResSDTO?    Item            = null,
        PkmHeldItemVersLi?  VersionDetails  = null
    );
}
