using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Move.Move
{
    public record MoveStatChangeSdto(
        [property: JsonPropertyName("change")]
        int?            Change = null,
        [property: JsonPropertyName("stat")]
        NamedApiResDto? Stat   = null
    );
}
