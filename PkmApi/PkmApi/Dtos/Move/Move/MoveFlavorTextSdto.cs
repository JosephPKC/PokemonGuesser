using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Move.Move
{
    public record MoveFlavorTextSdto(
        [property: JsonPropertyName("flavor_text")]
        string?         FlavorText   = null,
        [property: JsonPropertyName("language")]
        NamedApiResDto? Language     = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResDto? VersionGroup = null
    );
}
