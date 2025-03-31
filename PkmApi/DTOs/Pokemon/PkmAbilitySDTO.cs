using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    public record PkmAbilitySDTO(
        bool?            IsHidden   = null,
        int?             Slot       = null,
        NamedApiResSDTO? Ability    = null
    );
}
