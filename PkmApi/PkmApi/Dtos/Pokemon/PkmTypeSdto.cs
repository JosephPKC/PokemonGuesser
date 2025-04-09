using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Pokemon
{
    public record PkmTypeSdto(
        [property: JsonPropertyName("slot")]
        int?             Slot = null,
        [property: JsonPropertyName("type")]
        NamedApiResDto? Type = null
    );
}
