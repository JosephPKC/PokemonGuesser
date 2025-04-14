using System.Text.Json.Serialization;

using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Species
{
    public record GenusSdto(
        [property: JsonPropertyName("genus")]
        string?         Genus    = null,
        [property: JsonPropertyName("language")]
        NamedApiResDto? Language = null
    );
}
