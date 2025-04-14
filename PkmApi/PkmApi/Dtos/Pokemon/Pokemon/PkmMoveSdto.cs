using System.Collections.Immutable;
using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Pokemon
{
    using PkmMoveVersLi = IImmutableList<PkmMoveVersSdto>;
    public record PkmMoveSdto(
        [property: JsonPropertyName("move")]
        NamedApiResDto? Move                = null,
        [property: JsonPropertyName("version_group_details")]
        PkmMoveVersLi?   VersionGroupDetails = null
    );
}
