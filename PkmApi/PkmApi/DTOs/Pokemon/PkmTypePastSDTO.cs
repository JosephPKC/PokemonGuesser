using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Pokemon
{
    using PkmTypeLi = IImmutableList<PkmTypeSdto>;

    public record PkmTypePastSdto(
        [property: JsonPropertyName("generation")]
        NamedApiResDto? Generation = null,
        [property: JsonPropertyName("types")]
        PkmTypeLi?       Types      = null
    );
}
