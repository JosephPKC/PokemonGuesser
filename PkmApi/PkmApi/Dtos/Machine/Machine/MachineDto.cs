using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Machine.Machine
{
    public record MachineDto(
        [property: JsonPropertyName("id")]
        int             Id,
        [property: JsonPropertyName("item")]
        NamedApiResDto? Item         = null,
        [property: JsonPropertyName("move")]
        NamedApiResDto? Move         = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResDto? VersionGroup = null
    ) : IPkmApiDto;
}
