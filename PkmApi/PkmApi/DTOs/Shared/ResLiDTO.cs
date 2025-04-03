using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace PkmApi.DTOs.Shared
{
    using NamedApiResLi = IImmutableList<NamedApiResSDTO>;

    public record ResLiDTO(
        [property: JsonPropertyName("count")]
        int             Count,
        [property: JsonPropertyName("next")]
        string          Next,
        [property: JsonPropertyName("previous")]
        string          Previous,
        [property: JsonPropertyName("results")]
        NamedApiResLi   Results
    ) : IPkmApiDTO;
}
