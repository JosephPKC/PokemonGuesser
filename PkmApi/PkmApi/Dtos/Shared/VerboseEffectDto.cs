using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Shared
{
    public record VerboseEffectDto(
        [property: JsonPropertyName("effect")]
        string?         Effect      = null,
        [property: JsonPropertyName("short_effect")]
        string?         ShortEffect = null,
        [property: JsonPropertyName("language")]
        NamedApiResDto? Language    = null
    );
}
