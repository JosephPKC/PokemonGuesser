using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Type
{
    public record TypePokemonSdto(
        [property: JsonPropertyName("slot")]
        int?            Slot    = null,
        [property: JsonPropertyName("pokemon")]
        NamedApiResDto? Pokemon = null
    );
}
