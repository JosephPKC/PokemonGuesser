using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Ability
{
    public record AbilityFlavorTextSdto(
        [property: JsonPropertyName("flavor_text")]
        String?         FlavorText   = null,
        [property: JsonPropertyName("language")]
        NamedApiResDto? Language     = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResDto? VersionGroup = null
    );
}
