using System.Collections.Immutable;
using System.Text.Json.Serialization;
using PkmApi.DTOs;

namespace PkmApi.Test.Fakes
{
    public record BasicTestDTO(
        string                          Name,
        int                             Id,
        [property: JsonPropertyName("sub-data")]
        IImmutableList<BasicTestSDTO>   SubData
    ) : BasePkmDTO(Id, Name);

    public record BasicTestSDTO(
        [property: JsonPropertyName("value")]
        string Value  
    );
}
