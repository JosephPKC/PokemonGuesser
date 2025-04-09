using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Ability;
using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Move
{
    using VerboseEffLi      = IImmutableList<VerboseEffectDto>;
    using AbilityEffChLi    = IImmutableList<AbilityEffectChangeSdto>;
    using NamedApiResLi     = IImmutableList<NamedApiResDto>;
    using MoveFlavorTxtLi   = IImmutableList<MoveFlavorTextSdto>;
    using MachineVersDetLi  = IImmutableList<MachineVersionDetailDto>;
    using NameLi            = IImmutableList<NameDto>;
    using PastMoveStatValLi = IImmutableList<MovePastMoveStatValuesSdto>;
    using MoveStatChLi      = IImmutableList<MoveStatChangeSdto>;

    public record MoveDto(
        [property: JsonPropertyName("id")]
        int                       Id,
        [property: JsonPropertyName("name")]
        string                    Name,
        [property: JsonPropertyName("accuracy")]
        int?                      Accuracy          = null,
        [property: JsonPropertyName("effect_chance")]
        int?                      EffectChance      = null,
        [property: JsonPropertyName("pp")]
        int?                      Pp                = null,
        [property: JsonPropertyName("priority")]
        int?                      Priority          = null,
        [property: JsonPropertyName("power")]
        int?                      Power             = null,
        [property: JsonPropertyName("contest_combos")]
        MoveContestComboSetsSdto? ContestCombos     = null,
        [property: JsonPropertyName("contest_type")]
        NamedApiResDto?          ContestType        = null,
        [property: JsonPropertyName("contest_effect")]
        ApiResDto?               ContestEffect      = null,
        [property: JsonPropertyName("damage_class")]
        NamedApiResDto?          DamageClass        = null,
        [property: JsonPropertyName("effect_entries")]
        VerboseEffLi?            EffectEntries      = null,
        [property: JsonPropertyName("effect_changes")]
        AbilityEffChLi?          EffectChanges      = null,
        [property: JsonPropertyName("learned_by_pokemon")]
        NamedApiResLi?           LearnedByPokemon   = null,
        [property: JsonPropertyName("flavor_text_entries")]
        MoveFlavorTxtLi?         FlavorTextEntries  = null,
        [property: JsonPropertyName("generation")]
        NamedApiResDto?          Generation         = null,   //  Generation in which this was introduced.
        [property: JsonPropertyName("machines")]
        MachineVersDetLi?        Machines           = null,
        [property: JsonPropertyName("meta")]
        MoveMetaDataSdto?        Meta               = null,
        [property: JsonPropertyName("names")]
        NameLi?                  Names              = null,
        [property: JsonPropertyName("past_values")]
        PastMoveStatValLi?       PastValues         = null,
        [property: JsonPropertyName("stat_changes")]
        MoveStatChLi?            MoveStatChanges    = null,
        [property: JsonPropertyName("super_contest_effect")]
        ApiResDto?               SuperContestEffect = null,
        [property: JsonPropertyName("target")]
        NamedApiResDto?          Target             = null,
        [property: JsonPropertyName("type")]
        NamedApiResDto?          Type               = null
    ) : IPkmApiDto;
}
