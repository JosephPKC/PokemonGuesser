using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Utility
{
    public record VersionGameIndexDto(
        [property: JsonPropertyName("game_index")]
        int?             GameIndex = null,
        [property: JsonPropertyName("version")]
        NamedApiResDto? Version   = null
    );
}
