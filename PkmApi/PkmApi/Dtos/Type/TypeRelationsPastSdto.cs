using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Type
{
    public record TypeRelationsPastSdto(
        [property: JsonPropertyName("generation")]
        NamedApiResDto?    Generation      = null,
        [property: JsonPropertyName("damage_relations")]
        TypeRelationsSdto? DamageRelations = null
    );
}
