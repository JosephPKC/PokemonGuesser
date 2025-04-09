using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Move
{
    public record MoveContestComboSetsSdto(
        [property: JsonPropertyName("normal")]
        MoveContestComboDetailSdto? Normal = null,
        [property: JsonPropertyName("super")]
        MoveContestComboSetsSdto?   Super  = null
    );
}
