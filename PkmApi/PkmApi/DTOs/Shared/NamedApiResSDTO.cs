using System.Text.Json.Serialization;

namespace PkmApi.DTOs.Shared
{
    public record NamedApiResSDTO(
        [property: JsonPropertyName("name")]
        string  Name,
        [property: JsonPropertyName("url")]
        string  URL
    );
}
