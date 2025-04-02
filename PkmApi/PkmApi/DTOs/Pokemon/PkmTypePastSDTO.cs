using System.Collections.Immutable;
using System.Text.Json.Serialization;
using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    using PkmTypeLi = IImmutableList<PkmTypeSDTO>;

    public record PkmTypePastSDTO(
        [property: JsonPropertyName("generation")]
        NamedApiResSDTO?    Generation  = null,
        [property: JsonPropertyName("types")]
        PkmTypeLi?          Types       = null
    );
}
