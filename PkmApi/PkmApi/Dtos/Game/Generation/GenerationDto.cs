using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Game.Generation
{
    using NamedApiResLi = IImmutableList<NamedApiResDto>;
    using NameLi        = IImmutableList<NameDto>;

    public record GenerationDto(
        [property: JsonPropertyName("id")]
        int             Id,
        [property: JsonPropertyName("name")]
        string          Name,
        [property: JsonPropertyName("abilities")]
        NamedApiResLi?  Abilities      = null,
        [property: JsonPropertyName("names")]
        NameLi?         Names          = null,
        [property: JsonPropertyName("main_region")]
        NamedApiResDto? MainRegion     = null,
        [property: JsonPropertyName("moves")]
        NamedApiResLi?  Moves          = null,
        [property: JsonPropertyName("pokemon_species")]
        NamedApiResLi?  PokemonSpecies = null,
        [property: JsonPropertyName("types")]
        NamedApiResLi?  Types          = null,
        [property: JsonPropertyName("version_groups")]
        NamedApiResLi?  VersionGroups  = null
    ) : IPkmApiDto;
}
