using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Move.Move
{
    public record MoveContestComboSetsSdto(
        [property: JsonPropertyName("normal")]
        MoveContestComboDetailSdto? Normal = null,
        [property: JsonPropertyName("super")]
        MoveContestComboDetailSdto? Super  = null
    );
}
