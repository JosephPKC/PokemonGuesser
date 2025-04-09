using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Version
{
    using NameLi = IImmutableList<NameDto>;

    public record VersionDto(
        [property: JsonPropertyName("id")]
        int             Id,
        [property: JsonPropertyName("name")]
        string          Name,
        [property: JsonPropertyName("names")]
        NameLi?         Names        = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResDto? VersionGroup = null
    ) : IPkmApiDto;
}
