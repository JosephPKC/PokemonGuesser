using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Pokemon
{
    public record PkmCriesSdto(
        [property: JsonPropertyName("latest")]
        string? Latest = null,
        [property: JsonPropertyName("legacy")]
        string? Legacy = null
    );
}
