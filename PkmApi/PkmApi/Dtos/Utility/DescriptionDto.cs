using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Utility
{
    public record DescriptionDto(
        [property: JsonPropertyName("description")]
        string?         Description = null,
        [property: JsonPropertyName("language")]
        NamedApiResDto? Language    = null
    );
}

