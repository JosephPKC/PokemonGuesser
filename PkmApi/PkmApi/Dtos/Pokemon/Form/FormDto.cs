using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Form
{
    using PkmFormTypeLi = IImmutableList<PkmFormTypeSdto>;
    using NameLi        = IImmutableList<NameDto>;

    public record FormDto(
        [property: JsonPropertyName("id")]
        int                 Id,
        [property: JsonPropertyName("name")]
        string              Name,
        [property: JsonPropertyName("order")]
        int?                Order        = null,
        [property: JsonPropertyName("form_order")]
        int?                FormOrder    = null,
        [property: JsonPropertyName("is_default")]
        bool?               IsDefault    = null,
        [property: JsonPropertyName("is_battle_only")]
        bool?               IsBattleOnly = null,
        [property: JsonPropertyName("is_mega")]
        bool?               IsMega       = null,
        [property: JsonPropertyName("form_name")]
        string?             FormName     = null,
        [property: JsonPropertyName("pokemon")]
        NamedApiResDto?     Pokemon      = null,
        [property: JsonPropertyName("types")]
        PkmFormTypeLi?      Types        = null,
        [property: JsonPropertyName("sprites")]
        PkmFormSpritesSdto? Sprites      = null,
        [property: JsonPropertyName("version_group")]
        NamedApiResDto?     VersionGroup = null,
        [property: JsonPropertyName("names")]
        NameLi?             Names        = null,
        [property: JsonPropertyName("form_names")]
        NameLi?             FormNames    = null
    ) : IPkmApiDto;
}
