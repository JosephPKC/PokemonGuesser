using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    public record PkmHeldItemVersSDTO(
        NamedApiResSDTO?    Version = null,
        int?                Rarity = null
    );
}
