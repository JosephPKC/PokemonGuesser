using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Utility
{
    public record NameDto(
        [property: JsonPropertyName("name")]
        string?         Name     = null,
        [property: JsonPropertyName("language")]
        NamedApiResDto? Language = null
    );
}
