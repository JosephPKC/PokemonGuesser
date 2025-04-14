using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Utility
{
    public record FlavorTextDto(
        [property: JsonPropertyName("flavor_text")]
        string?         FlavorText = null,
        [property: JsonPropertyName("language")]
        NamedApiResDto? Language   = null,
        [property: JsonPropertyName("version")]
        NamedApiResDto? Version    = null
    );
}
