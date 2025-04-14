using System.Text.Json.Serialization;

using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Species
{
    public record PalParkEncounterAreaSdto(
        [property: JsonPropertyName("base_score")]
        int?            BaseScore = null,
        [property: JsonPropertyName("rate")]
        int?            Rate      = null,
        [property: JsonPropertyName("area")]
        NamedApiResDto? Area      = null
    );
}
