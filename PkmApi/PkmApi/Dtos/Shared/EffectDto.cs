using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Shared
{
    public record EffectDto(
        [property: JsonPropertyName("effect")]
        string?         Effect   = null,
        [property: JsonPropertyName("language")]
        NamedApiResDto? Language = null
    );
}
