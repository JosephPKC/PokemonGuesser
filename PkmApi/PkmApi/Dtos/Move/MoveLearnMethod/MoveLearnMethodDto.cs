using System.Collections.Immutable;
using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Move.MoveLearnMethod
{
    using DescLi        = IImmutableList<DescriptionDto>;
    using NamedApiResLi = IImmutableList<NamedApiResDto>;
    using NameLi        = IImmutableList<NameDto>;

    public record MoveLearnMethodDto(
        [property: JsonPropertyName("id")]
        int            Id,
        [property: JsonPropertyName("name")]
        string         Name,
        [property: JsonPropertyName("descriptions")]
        DescLi?        Descriptions  = null,
        [property: JsonPropertyName("names")]
        NameLi?        Names         = null,
        [property: JsonPropertyName("version_groups")]
        NamedApiResLi? VersionGroups = null
    ) : IPkmApiDto;
}
