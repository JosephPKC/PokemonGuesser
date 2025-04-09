using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Shared
{
    public record ApiResDto(
        [property: JsonPropertyName("url")]
        string URL
    );
}
