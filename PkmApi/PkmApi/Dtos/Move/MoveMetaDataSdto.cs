using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Move
{
    public record MoveMetaDataSdto(
        [property: JsonPropertyName("ailment")]
        NamedApiResDto? Ailment       = null,
        [property: JsonPropertyName("category")]
        NamedApiResDto? Category      = null,
        [property: JsonPropertyName("min_hits")]
        int?            MinHits       = null,
        [property: JsonPropertyName("max_hits")]
        int?            MaxHits       = null,
        [property: JsonPropertyName("min_turns")]
        int?            MinTurns      = null,
        [property: JsonPropertyName("max_turns")]
        int?            MaxTurns      = null,
        [property: JsonPropertyName("drain")]
        int?            Drain         = null,
        [property: JsonPropertyName("healing")]
        int?            Healing       = null,
        [property: JsonPropertyName("crit_rate")]
        int?            CritRate      = null,
        [property: JsonPropertyName("ailment_chance")]
        int?            AilmentChance = null,
        [property: JsonPropertyName("flinch_chance")]
        int?            FlinchChance  = null,
        [property: JsonPropertyName("stat_chance")]
        int?            StatChance    = null
    );
}
