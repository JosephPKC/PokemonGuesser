using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Pokemon
{
    public record PkmHeldItemVersSdto(
        [property: JsonPropertyName("version")]
        NamedApiResDto? Version = null,
        [property: JsonPropertyName("rarity")]
        int?             Rarity = null
    );
}
