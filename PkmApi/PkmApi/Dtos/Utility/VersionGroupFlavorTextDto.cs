using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Utility
{
    public record VersionGroupFlavorTextDto(
        [property: JsonPropertyName("language")]
        NamedApiResDto? Language     = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResDto? VersionGroup = null
    );
}
