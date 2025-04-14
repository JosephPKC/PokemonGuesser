using System.Collections.Immutable;
using System.Text.Json.Serialization;

using PkmApi.Dtos.Utility;

namespace PkmApi.Dtos.Pokemon.Species
{
    using PkmSpecDexEntryLi = IImmutableList<PkmSpeciesDexEntrySdto>;
    using NamedApiResLi     = IImmutableList<NamedApiResDto>;
    using NameLi            = IImmutableList<NameDto>;
    using PalParkEncAreaLi  = IImmutableList<PalParkEncounterAreaSdto>;
    using FlavorTxtLi       = IImmutableList<FlavorTextDto>;
    using DescLi            = IImmutableList<DescriptionDto>;
    using GenusLi           = IImmutableList<GenusSdto>;
    using PkmSpecVarietyLi  = IImmutableList<PkmSpeciesVarietySdto>;

    public record SpeciesDto(
        [property: JsonPropertyName("id")]
        int                Id,
        [property: JsonPropertyName("name")]
        string             Name,
        [property: JsonPropertyName("order")]
        int?               Order                = null,
        [property: JsonPropertyName("gender_rate")]
        int?               GenderRate           = null,
        [property: JsonPropertyName("capture_rate")]
        int?               CaptureRate          = null,
        [property: JsonPropertyName("base_happiness")]
        int?               BaseHappiness        = null,
        [property: JsonPropertyName("is_baby")]
        bool?              IsBaby               = null,
        [property: JsonPropertyName("is_legendary")]
        bool?              IsLegendary          = null,
        [property: JsonPropertyName("is_mythical")]
        bool?              IsMythical           = null,
        [property: JsonPropertyName("hatch_counter")]
        int?               HatchCounter         = null,
        [property: JsonPropertyName("has_gender_differences")]
        bool?              HasGenderDifferences = null,
        [property: JsonPropertyName("forms_switchable")]
        bool?              FormsSwitchable      = null,
        [property: JsonPropertyName("growth_rate")]
        NamedApiResDto?    GrowthRate           = null,
        [property: JsonPropertyName("pokedex_numbers")]
        PkmSpecDexEntryLi? PokedexNumbers       = null,
        [property: JsonPropertyName("egg_groups")]
        NamedApiResLi?     EggGroups            = null,
        [property: JsonPropertyName("color")]
        NamedApiResDto?    Color                = null,
        [property: JsonPropertyName("shape")]
        NamedApiResDto?    Shape                = null,
        [property: JsonPropertyName("evolves_from_species")]
        NamedApiResDto?    EvolvesFromSpecies   = null,
        [property: JsonPropertyName("evolution_chain")]
        ApiResDto?         EvolutionChain       = null,
        [property: JsonPropertyName("habitat")]
        NamedApiResDto?    Habitat              = null,
        [property: JsonPropertyName("generation")]
        NamedApiResDto?    Generation           = null,
        [property: JsonPropertyName("names")]
        NameLi?            Names                = null,
        [property: JsonPropertyName("pal_park_encounters")]
        PalParkEncAreaLi?  PalParkEncounters    = null,
        [property: JsonPropertyName("flavor_text_entries")]
        FlavorTxtLi?       FlavorTextEntries    = null,
        [property: JsonPropertyName("form_descriptions")]
        DescLi?            FormDescriptions     = null,
        [property: JsonPropertyName("genera")]
        GenusLi?           Genera               = null,
        [property: JsonPropertyName("varieties")]
        PkmSpecVarietyLi?  Varieties            = null
    ) : IPkmApiDto;
}
