using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    using PkmHeldItemVersLi = IImmutableList<PkmHeldItemVersSDTO>;

    public record PkmHeldItemSDTO(
        [property: JsonPropertyName("item")]
        NamedApiResSDTO?    Item            = null,
        [property: JsonPropertyName("version_details")]
        PkmHeldItemVersLi?  VersionDetails  = null
    );
}
