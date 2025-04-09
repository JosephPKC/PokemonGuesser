using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Type
{
    public record TypePokemonSdto(
        [property: JsonPropertyName("slot")]
        int?            Slot    = null,
        [property: JsonPropertyName("pokemon")]
        NamedApiResDto? Pokemon = null
    );
}
