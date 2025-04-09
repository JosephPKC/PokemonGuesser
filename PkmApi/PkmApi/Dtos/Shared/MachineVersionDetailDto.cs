using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Shared
{
    public record MachineVersionDetailDto(
        [property: JsonPropertyName("machine")]
        ApiResDto?      Machine      = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResDto? VersionGroup = null
    );
}
