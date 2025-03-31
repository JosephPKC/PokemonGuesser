using System.Collections.Immutable;

using PkmApi.DTOs.Shared;

namespace PkmApi.DTOs.Pokemon
{
    using PkmTypeLi = IImmutableList<PkmTypeSDTO>;

    public record PkmTypePastSDTO(
        NamedApiResSDTO?    Generation  = null,
        PkmTypeLi?          Types       = null
    );
}
