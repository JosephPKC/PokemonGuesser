using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Shared
{
    using NamedApiResLi = IImmutableList<NamedApiResDto>;

    public record ResLiDto(
        [property: JsonPropertyName("count")]
        int?           Count    = null,
        [property: JsonPropertyName("next")]
        string?        Next     = null,
        [property: JsonPropertyName("previous")]
        string?        Previous = null,
        [property: JsonPropertyName("results")]
        NamedApiResLi? Results  = null
    ) : IPkmApiDto;
}
