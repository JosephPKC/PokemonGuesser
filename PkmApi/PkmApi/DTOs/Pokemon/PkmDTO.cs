using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    using PkmAbilityLi          = IImmutableList<PkmAbilitySDTO>;
    using NamedApiResLi         = IImmutableList<NamedApiResSDTO>;
    using VersionGameIndexLi    = IImmutableList<VersionGameIndexSDTO>;
    using PkmHeldItemLi         = IImmutableList<PkmHeldItemSDTO>;
    using PkmMoveLi             = IImmutableList<PkmMoveSDTO>;
    using PkmTypePastLi         = IImmutableList<PkmTypePastSDTO>;
    using PkmStatLi             = IImmutableList<PkmStatSDTO>;
    using PkmTypeLi             = IImmutableList<PkmTypeSDTO>;

    public record PkmDTO(
        int                     Id,
        string                  Name,
        [property: JsonPropertyName("base_experience")]
        int?                    BaseExperience          = null,
        [property: JsonPropertyName("height")]
        int?                    Height                  = null,
        [property: JsonPropertyName("is_default")]
        bool?                   IsDefault               = null, //  The default Pokemon for its species. There can only be ONE default for each species.
        [property: JsonPropertyName("order")]
        int?                    Order                   = null, //  Order for sorting. Almost national order, except that families are grouped together.
        [property: JsonPropertyName("weight")]
        int?                    Weight                  = null,
        [property: JsonPropertyName("abilities")]
        PkmAbilityLi?           Abilities               = null,
        [property: JsonPropertyName("forms")]
        NamedApiResLi?          Forms                   = null,
        [property: JsonPropertyName("game_indices")]
        VersionGameIndexLi?     GameIndices             = null,
        [property: JsonPropertyName("held_items")]
        PkmHeldItemLi?          HeldItems               = null,
        [property: JsonPropertyName("location_area_encounters")]
        string?                 LocationAreaEncounters  = null, //  A link to the list of location areas.
        [property: JsonPropertyName("moves")]
        PkmMoveLi?              Moves                   = null,
        [property: JsonPropertyName("past_types")]
        PkmTypePastLi?          PastTypes               = null,
        [property: JsonPropertyName("sprites")]
        PkmSpritesSDTO?         Sprites                 = null, //  Urls for sprite images. Located here: https://github.com/PokeAPI/sprites
        [property: JsonPropertyName("cries")]
        PkmCriesSDTO?           Cries                   = null, //  Urls for cry audio files. Located here: https://github.com/PokeAPI/cries
        [property: JsonPropertyName("species")]
        NamedApiResSDTO?        Species                 = null, //  Res Url for the associated Pokemon species.
        [property: JsonPropertyName("stats")]
        PkmStatLi?              Stats                   = null,
        [property: JsonPropertyName("types")]
        PkmTypeLi?              Types                   = null
    ) : BasePkmDTO(Id, Name);
}
