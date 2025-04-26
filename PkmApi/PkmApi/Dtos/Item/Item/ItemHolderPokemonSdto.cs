using System.Collections.Immutable;
using System.Text.Json.Serialization;
using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Item.Item
{
    using ItemHolderPkmVersDetailLi = IImmutableList<ItemHolderPkmVersionDetailSdto>;

    public record ItemHolderPokemonSdto(
        [property: JsonPropertyName("pokemon")]
        NamedApiResDto?            Pokemon        = null,
        [property: JsonPropertyName("version_details")]
        ItemHolderPkmVersDetailLi? VersionDetails = null
    ) : IPkmApiDto;
}
