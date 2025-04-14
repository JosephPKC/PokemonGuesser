using System.Text.Json.Serialization;

using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Species
{
    public record PkmSpeciesVarietySdto(
        [property: JsonPropertyName("is_default")]
        bool?           IsDefault = null,
        [property: JsonPropertyName("pokemon")]
        NamedApiResDto? Pokemon   = null
    );
}
