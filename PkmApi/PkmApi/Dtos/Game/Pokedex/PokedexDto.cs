using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Game.Pokedex
{
    using DescLi        = IImmutableList<DescriptionDto>;
    using NameLi        = IImmutableList<NameDto>;
    using PkmEntryLi    = IImmutableList<PkmEntrySdto>;
    using NamedApiResLi = IImmutableList<NamedApiResDto>;

    public record PokedexDto(
        [property: JsonPropertyName("id")]
        int             Id,
        [property: JsonPropertyName("name")]
        string          Name,
        [property: JsonPropertyName("is_main_series")]
        bool?           IsMainSeries   = null,
        [property: JsonPropertyName("descriptions")]
        DescLi?         Descriptions   = null,
        [property: JsonPropertyName("names")]
        NameLi?         Names          = null,
        [property: JsonPropertyName("pokemon_entries")]
        PkmEntryLi?     PokemonEntries = null,
        [property: JsonPropertyName("region")]
        NamedApiResDto? Region         = null,
        [property: JsonPropertyName("version_groups")]
        NamedApiResLi?  VersionGroups  = null
    ) : IPkmApiDto;
}
