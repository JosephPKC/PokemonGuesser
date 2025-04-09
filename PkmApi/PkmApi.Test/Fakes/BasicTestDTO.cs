using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos;

namespace PkmApi.Test.Fakes
{
    public record BasicTestDto(
        [property: JsonPropertyName("name")]
        string                          Name,
        [property: JsonPropertyName("id")]
        int                             Id,
        [property: JsonPropertyName("sub-data")]
        IImmutableList<BasicTestSdto>   SubData
    ) : IPkmApiDto;

    public record BasicTestSdto(
        [property: JsonPropertyName("value")]
        string Value  
    );
}
