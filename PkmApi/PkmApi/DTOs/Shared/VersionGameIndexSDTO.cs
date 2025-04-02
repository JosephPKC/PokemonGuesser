using System.Text.Json.Serialization;

namespace PkmApi.DTOs.Shared
{
    public record VersionGameIndexSDTO(
        [property: JsonPropertyName("game_index")]
        int?             GameIndex = null,
        [property: JsonPropertyName("version")]
        NamedApiResSDTO? Version = null
    );
}
