using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    using PkmMoveVersLi = IImmutableList<PkmMoveVersSDTO>;
    public record PkmMoveSDTO(
        [property: JsonPropertyName("move")]
        NamedApiResSDTO?    Move                = null,
        [property: JsonPropertyName("version_group_details")]
        PkmMoveVersLi?      VersionGroupDetails = null
    );
}
