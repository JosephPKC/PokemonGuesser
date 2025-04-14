using System.Text.Json.Serialization;

using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Species
{
    public record PkmSpeciesDexEntrySdto(
        [property: JsonPropertyName("entry_number")]
        int?            EntryNumber = null,
        [property: JsonPropertyName("pokedex")]
        NamedApiResDto? Pokedex     = null
    );
}
