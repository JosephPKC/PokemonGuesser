using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.DTOs;

namespace PkmApi.Test.Fakes
{
    public record BasicTestDTO(
        [property: JsonPropertyName("name")]
        string                          Name,
        [property: JsonPropertyName("id")]
        int                             Id,
        [property: JsonPropertyName("sub-data")]
        IImmutableList<BasicTestSDTO>   SubData
    ) : IPkmApiDTO;

    public record BasicTestSDTO(
        [property: JsonPropertyName("value")]
        string Value  
    );
}
