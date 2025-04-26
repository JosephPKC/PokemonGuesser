using System.Collections.Immutable;
using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Item.Item
{
    using NamedApiResLi       = IImmutableList<NamedApiResDto>;
    using VerboseEffLi        = IImmutableList<VerboseEffectDto>;
    using VersGrpFlavorTxtLi  = IImmutableList<VersionGroupFlavorTextDto>;
    using GenGameIndLi        = IImmutableList<GenerationGameIndexDto>;
    using NameLi              = IImmutableList<NameDto>;
    using ItemHolderPkmLi     = IImmutableList<ItemHolderPokemonSdto>;
    using MachineVersDetailLi = IImmutableList<MachineVersionDetailDto>;

    public record ItemDto(
        [property: JsonPropertyName("id")]
        int                  Id,
        [property: JsonPropertyName("name")]
        string               Name,
        [property: JsonPropertyName("cost")]
        int?                 Cost              = null,
        [property: JsonPropertyName("fling_power")]
        int?                 FlingPower        = null,
        [property: JsonPropertyName("fling_effect")]
        NamedApiResDto?      FlingEffect       = null,
        [property: JsonPropertyName("attributes")]
        NamedApiResLi?       Attributes        = null,
        [property: JsonPropertyName("category")]
        NamedApiResDto?      Category          = null,
        [property: JsonPropertyName("effect_entries")]
        VerboseEffLi?        EffectEntries     = null,
        [property: JsonPropertyName("flavor_text_entries")]
        VersGrpFlavorTxtLi?  FlavorTextEntries = null,
        [property: JsonPropertyName("game_indices")]
        GenGameIndLi?        GameIndices       = null,
        [property: JsonPropertyName("names")]
        NameLi?              Names             = null,
        [property: JsonPropertyName("sprites")]
        ItemSpritesSdto?     Sprites           = null,
        [property: JsonPropertyName("held_by_pokemon")]
        ItemHolderPkmLi?     HeldByPokemon     = null,
        [property: JsonPropertyName("baby_trigger_for")]
        ApiResDto?           BabyTriggerFor    = null,
        [property: JsonPropertyName("machines")]
        MachineVersDetailLi? Machines          = null
    ) : IPkmApiDto;
}
