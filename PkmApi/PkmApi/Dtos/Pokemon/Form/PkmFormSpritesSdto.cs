using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Pokemon.Form
{
    public record PkmFormSpritesSdto(
        [property: JsonPropertyName("front_default")]
        string? FrontDefault = null,
        [property: JsonPropertyName("front_shiny")]
        string? FrontShiny   = null,
        [property: JsonPropertyName("back_default")]
        string? BackDefault  = null,
        [property: JsonPropertyName("back_shiny")]
        string? BackShiny    = null
    );
}
