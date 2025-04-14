using System.Collections.Immutable;
using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Move.MoveDamageClass
{
    using DescLi        = IImmutableList<DescriptionDto>;
    using NamedApiResLi = IImmutableList<NamedApiResDto>;
    using NameLi        = IImmutableList<NameDto>;

    public record MoveDamageClassDto(
        [property: JsonPropertyName("id")]
        int            Id,
        [property: JsonPropertyName("name")]
        string         Name,
        [property: JsonPropertyName("descriptions")]
        DescLi?        Descriptions = null,
        [property: JsonPropertyName("moves")]
        NamedApiResLi? Moves        = null,
        [property: JsonPropertyName("names")]
        NameLi?        Names        = null
    ) : IPkmApiDto;
}
