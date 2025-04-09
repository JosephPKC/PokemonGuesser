using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Pokemon
{
    public record PkmStatSdto(
        [property: JsonPropertyName("stat")]
        NamedApiResDto? Stat     = null,
        [property: JsonPropertyName("effort")]
        int?             Effort   = null,
        [property: JsonPropertyName("base_stat")]
        int?             BaseStat = null
    );
}
