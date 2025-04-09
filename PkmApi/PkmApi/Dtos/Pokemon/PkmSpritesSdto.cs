using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Pokemon
{
    public record PkmSpritesSdto(
        [property: JsonPropertyName("front_default")]
        string? FrontDefault     = null,
        [property: JsonPropertyName("front_shiny")]
        string? FrontShiny       = null,
        [property: JsonPropertyName("front_female")]
        string? FrontFemale      = null,
        [property: JsonPropertyName("front_shiny_female")]
        string? FrontShinyFemale = null,
        [property: JsonPropertyName("back_default")]
        string? BackDefault      = null,
        [property: JsonPropertyName("back_shiny")]
        string? BackShiny        = null,
        [property: JsonPropertyName("back_female")]
        string? BackFemale       = null,
        [property: JsonPropertyName("back_shiny_female")]
        string? BackShinyFemale  = null
    );
}
