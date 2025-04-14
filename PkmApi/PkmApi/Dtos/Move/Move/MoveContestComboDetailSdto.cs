using System.Collections.Immutable;
using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Move.Move
{
    using NamedApiResLi = IImmutableList<NamedApiResDto>;
    public record MoveContestComboDetailSdto(
        [property: JsonPropertyName("use_before")]
        NamedApiResLi? UseBefore = null,
        [property: JsonPropertyName("use_after")]
        NamedApiResLi? UseAfter  = null
    );
}
