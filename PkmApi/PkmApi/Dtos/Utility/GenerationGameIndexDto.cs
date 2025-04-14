using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Utility
{
    public record GenerationGameIndexDto(
        [property: JsonPropertyName("game_index")]
        int?            GameIndex  = null,
        [property: JsonPropertyName("generation")]
        NamedApiResDto? Generation = null
    );
}
