using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Shared
{
    public record NamedApiResDto(
        [property: JsonPropertyName("name")]
        string Name,
        [property: JsonPropertyName("url")]
        string URL
    );
}
