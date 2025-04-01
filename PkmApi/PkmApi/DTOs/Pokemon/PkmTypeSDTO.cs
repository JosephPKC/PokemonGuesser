using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    public record PkmTypeSDTO(
        int?                Slot = null,
        NamedApiResSDTO?    Type = null
    );
}
