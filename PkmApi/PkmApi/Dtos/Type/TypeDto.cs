using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Type
{
    using TypeRelationsPastLi = IImmutableList<TypeRelationsPastSdto>;
    using GenerationGameIndLi = IImmutableList<GenerationGameIndexDto>;
    using NameLi              = IImmutableList<NameDto>;
    using TypePokemonLi       = IImmutableList<TypePokemonSdto>;
    using NamedApiResLi       = IImmutableList<NamedApiResDto>;

    public record TypeDto(
        [property: JsonPropertyName("id")]
        int                     Id,
        [property: JsonPropertyName("name")]
        string                  Name,
        [property: JsonPropertyName("damage_relations")]
        TypeRelationsSdto?      DamageRelations     = null,
        [property: JsonPropertyName("past_damage_relations")]
        TypeRelationsPastLi?    PastDamageRelations = null,
        [property: JsonPropertyName("game_indices")]
        GenerationGameIndLi?    GameIndices         = null,
        [property: JsonPropertyName("generation")]
        NamedApiResDto?         Generation          = null,
        [property: JsonPropertyName("move_damage_class")]
        NamedApiResDto?         MoveDamageClass     = null,
        [property: JsonPropertyName("names")]
        NameLi?                 Names               = null,
        [property: JsonPropertyName("pokemon")]
        TypePokemonLi?          Pokemon             = null,
        [property: JsonPropertyName("moves")]
        NamedApiResLi?          Moves               = null
    ) : IPkmApiDto;
}
