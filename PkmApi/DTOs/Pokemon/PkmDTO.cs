using System.Collections.Immutable;

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
        int?                    BaseExperience          = null, 
        int?                    Height                  = null,
        bool?                   IsDefault               = null, //  The default Pokemon for its species. There can only be ONE default for each species.
        int?                    Order                   = null, //  Order for sorting. Almost national order, except that families are grouped together.
        int?                    Weight                  = null,
        PkmAbilityLi?           Abilities               = null,
        NamedApiResLi?          Forms                   = null,
        VersionGameIndexLi?     GameIndices             = null,
        PkmHeldItemLi?          HeldItems               = null,
        string?                 LocationAreaEncounters  = null, //  A link to the list of location areas.
        PkmMoveLi?              Moves                   = null,
        PkmTypePastLi?          Type                    = null,
        PkmSpritesSDTO?         Sprites                 = null, //  Urls for sprite images. Located here: https://github.com/PokeAPI/sprites
        PkmCriesSDTO?           Criess                  = null, //  Urls for cry audio files. Located here: https://github.com/PokeAPI/cries
        NamedApiResSDTO?        Species                 = null, //  Res Url for the associated Pokemon species.
        PkmStatLi?              Stats                   = null,
        PkmTypeLi?              Types                   = null
    ) : BasePkmDTO(Id, Name);
}
