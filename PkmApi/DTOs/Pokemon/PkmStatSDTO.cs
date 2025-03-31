using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    public record PkmStatSDTO(
        NamedApiResSDTO?    Stat        = null,
        int?                Effort      = null,
        int?                BaseStat    = null
    );
}
