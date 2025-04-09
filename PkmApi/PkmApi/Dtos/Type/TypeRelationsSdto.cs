using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Type
{
    using NamedApiResLi = IImmutableList<NamedApiResDto>;

    public record TypeRelationsSdto(
        [property: JsonPropertyName("no_damage_to")]
        NamedApiResLi? NoDamageTo       = null,
        [property: JsonPropertyName("half_damage_to")]
        NamedApiResLi? HalfDamageTo     = null,
        [property: JsonPropertyName("double_damage_to")]
        NamedApiResLi? DoubleDamageTo   = null,
        [property: JsonPropertyName("no_damage_from")]
        NamedApiResLi? NoDamageFrom     = null,
        [property: JsonPropertyName("half_damage_from")]
        NamedApiResLi? HalfDamageFrom   = null,
        [property: JsonPropertyName("double_damage_from")]
        NamedApiResLi? DoubleDamageFrom = null
    );
}
