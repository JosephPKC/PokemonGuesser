using System.Text.Json.Serialization;
using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    public record PkmHeldItemVersSDTO(
        [property: JsonPropertyName("version")]
        NamedApiResSDTO?    Version = null,
        [property: JsonPropertyName("rarity")]
        int?                Rarity = null
    );
}
