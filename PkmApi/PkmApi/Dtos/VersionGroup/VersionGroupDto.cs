using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.VersionGroup
{
    using NamedApiResLi = IImmutableList<NamedApiResDto>;

    public record VersionGroupDto(
        [property: JsonPropertyName("id")]
        int             Id,
        [property: JsonPropertyName("name")]
        string          Name,
        [property: JsonPropertyName("names")]
        int?            Order            = null,
        [property: JsonPropertyName("generation")]
        NamedApiResDto? Generation       = null,
        [property: JsonPropertyName("move_learn_methods")]
        NamedApiResLi?  MoveLearnMethods = null,
        [property: JsonPropertyName("pokedexes")]
        NamedApiResLi?  Pokedexes        = null,
        [property: JsonPropertyName("regions")]
        NamedApiResLi?  Regions          = null,
        [property: JsonPropertyName("version")]
        NamedApiResLi?  Versions         = null
    ) : IPkmApiDto;
}
