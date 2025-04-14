using System.Text.Json.Serialization;

namespace PkmApi.Dtos.Pokemon.Form
{
    public record PkmFormSpritesSdto(
        [property: JsonPropertyName("front_default")]
        string? FrontDefault,
        [property: JsonPropertyName("front_shiny")]
        string? FrontShiny,
        [property: JsonPropertyName("back_default")]
        string? BackDefault,
        [property: JsonPropertyName("back_shiny")]
        string? BackShiny
    );
}
