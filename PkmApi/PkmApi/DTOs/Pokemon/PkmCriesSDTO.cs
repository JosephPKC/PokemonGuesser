using System.Text.Json.Serialization;

namespace PkmApi.DTOs.Pokemon
{
    public record PkmCriesSDTO(
        [property: JsonPropertyName("latest")]
        string? Latest = null,
        [property: JsonPropertyName("legacy")]
        string? Legacy = null
    );
}
