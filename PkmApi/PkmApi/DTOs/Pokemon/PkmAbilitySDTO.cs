using System.Text.Json.Serialization;
using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    public record PkmAbilitySDTO(
        [property: JsonPropertyName("is_hidden")]
        bool?            IsHidden   = null,
        [property: JsonPropertyName("slot")]
        int?             Slot       = null,
        [property: JsonPropertyName("ability")]
        NamedApiResSDTO? Ability    = null
    );
}
