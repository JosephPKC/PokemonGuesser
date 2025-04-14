using System.Text.Json.Serialization;

using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Game.Pokedex
{
    public record PkmEntrySdto(
        [property: JsonPropertyName("entry_number")]
        int?            EntryNumber    = null,
        [property: JsonPropertyName("pokemon_species")]
        NamedApiResDto? PokemonSpecies = null
    );
}
