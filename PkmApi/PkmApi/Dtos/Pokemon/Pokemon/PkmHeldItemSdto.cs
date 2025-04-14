using System.Collections.Immutable;
using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Pokemon
{
    using PkmHeldItemVersLi = IImmutableList<PkmHeldItemVersSdto>;

    public record PkmHeldItemSdto(
        [property: JsonPropertyName("item")]
        NamedApiResDto?   Item           = null,
        [property: JsonPropertyName("version_details")]
        PkmHeldItemVersLi? VersionDetails = null
    );
}
