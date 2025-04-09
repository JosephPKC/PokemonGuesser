using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Shared
{
    public record NameDto(
        [property: JsonPropertyName("name")]
        String?         Name     = null,
        [property: JsonPropertyName("language")]
        NamedApiResDto? Language = null
    );
}
