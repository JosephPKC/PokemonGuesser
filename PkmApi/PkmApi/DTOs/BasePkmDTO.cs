using System.Text.Json.Serialization;

namespace PkmApi.DTOs
{
    public abstract record BasePkmDTO(
        [property: JsonPropertyName("id")]
        int     Id,
        [property: JsonPropertyName("name")]
        string  Name
    );
}
