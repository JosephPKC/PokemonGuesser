using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Move
{
    public record MoveStatChangeSdto(
        [property: JsonPropertyName("change")]
        int?            Change = null,
        [property: JsonPropertyName("stat")]
        NamedApiResDto? Stat   = null
    );
}
