using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Ability
{
    using EffectLi = IImmutableList<EffectDto>;

    public record AbilityEffectChangeSdto(
        [property: JsonPropertyName("effect_entries")]
        EffectLi?       EffectEntries = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResDto? VersionGroup  = null
    );
}
