using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    public record PkmMoveVersSDTO(
        NamedApiResSDTO?    MoveLearnMethod = null,
        NamedApiResSDTO?    VersionGroup    = null,
        int?                LevelLearnedAt  = null
    );
}
