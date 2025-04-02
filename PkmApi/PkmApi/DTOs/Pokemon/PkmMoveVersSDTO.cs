using System.Text.Json.Serialization;

using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    public record PkmMoveVersSDTO(
        [property: JsonPropertyName("move_learn_method")]
        NamedApiResSDTO?    MoveLearnMethod = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResSDTO?    VersionGroup    = null,
        [property: JsonPropertyName("level_learned_at")]
        int?                LevelLearnedAt  = null
    );
}
