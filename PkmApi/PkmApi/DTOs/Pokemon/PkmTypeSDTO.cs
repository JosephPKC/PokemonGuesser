using System.Text.Json.Serialization;

using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    public record PkmTypeSDTO(
        [property: JsonPropertyName("slot")]
        int?                Slot = null,
        [property: JsonPropertyName("type")]
        NamedApiResSDTO?    Type = null
    );
}
