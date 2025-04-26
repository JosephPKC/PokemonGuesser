using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Item.Item
{
    public record ItemHolderPkmVersionDetailSdto(
        [property: JsonPropertyName("rarity")]
        int?            Rarity  = null,
        [property: JsonPropertyName("version")]
        NamedApiResDto? Version = null
    ) : IPkmApiDto;
}
