namespace PkmApi.DTOs.Pokemon
{
    public record PkmSpritesSDTO(
        string?     FrontDefault        = null,
        string?     FrontShiny          = null,
        string?     FrontFemale         = null,
        string?     FrontShinyFemale    = null,
        string?     BackDefault         = null,
        string?     BackShiny           = null,
        string?     BackFemale          = null,
        string?     BackShinyFemale     = null
    );
}
