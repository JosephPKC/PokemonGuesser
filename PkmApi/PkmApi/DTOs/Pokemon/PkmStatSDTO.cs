using System.Text.Json.Serialization;
using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    public record PkmStatSDTO(
        [property: JsonPropertyName("stat")]
        NamedApiResSDTO?    Stat        = null,
        [property: JsonPropertyName("effort")]
        int?                Effort      = null,
        [property: JsonPropertyName("base_stat")]
        int?                BaseStat    = null
    );
}
