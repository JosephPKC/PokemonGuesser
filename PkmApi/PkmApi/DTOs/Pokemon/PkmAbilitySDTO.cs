using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Pokemon
{
    public record PkmAbilitySdto(
        [property: JsonPropertyName("is_hidden")]
        bool?            IsHidden = null,
        [property: JsonPropertyName("slot")]
        int?             Slot     = null,
        [property: JsonPropertyName("ability")]
        NamedApiResDto? Ability  = null
    );
}
