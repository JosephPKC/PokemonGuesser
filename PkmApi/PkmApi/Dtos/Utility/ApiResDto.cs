using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Utility
{
    public record ApiResDto(
        [property: JsonPropertyName("url")]
        string URL
    );
}
