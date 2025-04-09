using System.Text.Json.Serialization;

using PkmApi.Dtos.Shared;

namespace PkmApi.Dtos.Pokemon
{
    public record PkmMoveVersSdto(
        [property: JsonPropertyName("move_learn_method")]
        NamedApiResDto? MoveLearnMethod = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResDto? VersionGroup    = null,
        [property: JsonPropertyName("level_learned_at")]
        int?             LevelLearnedAt  = null
    );
}
