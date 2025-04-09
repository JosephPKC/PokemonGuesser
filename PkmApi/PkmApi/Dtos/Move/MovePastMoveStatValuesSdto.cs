using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Move
{
    using VerboseEffLi = IImmutableList<VerboseEffectDto>;

    public record MovePastMoveStatValuesSdto(
        [property: JsonPropertyName("accuracy")]
        int?            Accuracy      = null,
        [property: JsonPropertyName("effect_chance")]
        int?            EffectChance  = null,
        [property: JsonPropertyName("power")]
        int?            Power         = null,
        [property: JsonPropertyName("pp")]
        int?            Pp            = null,
        [property: JsonPropertyName("effect_entries")]
        VerboseEffLi?   EffectEntries = null,
        [property: JsonPropertyName("type")]
        NamedApiResDto? Type          = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResDto? VersionGroup  = null
    );
}
