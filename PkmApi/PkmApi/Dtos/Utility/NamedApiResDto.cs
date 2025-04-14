using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Utility
{
    public record NamedApiResDto(
        [property: JsonPropertyName("name")]
        string Name,
        [property: JsonPropertyName("url")]
        string URL
    );
}
