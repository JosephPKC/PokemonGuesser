using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Ability
{
    using NameLi             = IImmutableList<NameDto>;
    using VerboseEffLi       = IImmutableList<VerboseEffectDto>;
    using AbilityEffChLi     = IImmutableList<AbilityEffectChangeSdto>;
    using AbilityFlavorTxtLi = IImmutableList<AbilityFlavorTextSdto>;
    using AbilityPkmLi       = IImmutableList<AbilityPokemonSdto>;

    public record AbilityDto(
        [property: JsonPropertyName("id")]
        int                 Id,
        [property: JsonPropertyName("name")]
        string              Name,
        [property: JsonPropertyName("is_main_series")]
        bool?               IsMainSeries      = null,
        [property: JsonPropertyName("generation")]
        NamedApiResDto?     Generation        = null,
        [property: JsonPropertyName("names")]
        NameLi?             Names             = null,
        [property: JsonPropertyName("effect_entries")]
        VerboseEffLi?       EffectEntries     = null,
        [property: JsonPropertyName("effect_changes")]
        AbilityEffChLi?     EffectChanges     = null,
        [property: JsonPropertyName("flavor_text_entries")]
        AbilityFlavorTxtLi? FlavorTextEntries = null,
        [property: JsonPropertyName("pokemon")]
        AbilityPkmLi?       Pokemon           = null
    ) : IPkmApiDto;
}
