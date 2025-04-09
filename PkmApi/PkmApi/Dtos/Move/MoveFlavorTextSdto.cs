using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Move
{
    public record MoveFlavorTextSdto(
        [property: JsonPropertyName("flavor_text")]
        String?         FlavorText   = null,
        [property: JsonPropertyName("language")]
        NamedApiResDto? Language     = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResDto? VersionGroup = null
    );
}
