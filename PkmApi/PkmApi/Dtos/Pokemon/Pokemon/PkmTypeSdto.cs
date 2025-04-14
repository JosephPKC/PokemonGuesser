using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Pokemon
{
    public record PkmTypeSdto(
        [property: JsonPropertyName("slot")]
        int?             Slot = null,
        [property: JsonPropertyName("type")]
        NamedApiResDto? Type = null
    );
}
