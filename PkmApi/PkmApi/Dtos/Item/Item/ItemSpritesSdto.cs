using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Item.Item
{
    public record ItemSpritesSdto(
        [property: JsonPropertyName("default")]
        string? Default = null
    ) : IPkmApiDto;
}
