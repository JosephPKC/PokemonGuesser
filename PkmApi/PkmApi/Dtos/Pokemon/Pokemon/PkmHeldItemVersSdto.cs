using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Pokemon
{
    public record PkmHeldItemVersSdto(
        [property: JsonPropertyName("version")]
        NamedApiResDto? Version = null,
        [property: JsonPropertyName("rarity")]
        int?             Rarity = null
    );
}
