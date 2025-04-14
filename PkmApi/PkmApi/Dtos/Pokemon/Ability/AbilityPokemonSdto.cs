using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Ability
{
    public record AbilityPokemonSdto(
        [property: JsonPropertyName("is_hidden")]
        bool?           IsHidden = null,
        [property: JsonPropertyName("slot")]
        int?            Slot     = null,
        [property: JsonPropertyName("pokemon")]
        NamedApiResDto? Pokemon  = null
    );
}
