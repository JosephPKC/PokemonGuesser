using PkmApi.Dtos.Pokemon.Species;

namespace PkmApiTestDtos.Pokemon
{
    internal class SpeciesDtoTestBuilder : IDtoTestBuilder<SpeciesDto>
    {
        #region IDtoTestBuilder<SpeciesDto>
        public SpeciesDto GetBasic()
        {
            return new(1, "species");
        }

        public SpeciesDto GetEmpty()
        {
            return new(
                1, "species", 1, 1, 1, 1, true, true, true, 1, true, true,
                new("growth-rate", "growth-rates/1"), [], [], new("color", "colors/1"),
                new("shape", "shapes/1"), new("evolves-from-species", "species/1"),
                new("evolves/1"), new("habitat", "habitats/1"), new("gen", "gens/1"),
                [], [], [], [], [], []
            );
        }

        public SpeciesDto GetFull()
        {
            return new(
                1, "species", 1, 1, 1, 1, true, true, true, 1, true, true,
                new("growth-rate", "growth-rates/1"), 
                [new(1, new("pokedex", "pokedexes/1"))], 
                [new("egg-group", "egg-groups/1")], 
                new("color", "colors/1"),
                new("shape", "shapes/1"), 
                new("evolves-from-species", "species/1"),
                new("evolves/1"), 
                new("habitat", "habitats/1"), 
                new("gen", "gens/1"),
                [new("name", new("language", "languages/1"))], 
                [new(1, 1, new("area", "areas/1"))], 
                [new("flavor-text", new("language", "languages/1"), new("version-group", "version-groups/1"))], 
                [new("desc", new("language", "languages/1"))], 
                [new("genus", new("language", "languages/1"))], 
                [new(true, new("pkm", "pkms/1"))]
            );
        }

        public SpeciesDto GetShallow()
        {
            return new(
                1, "species", 1, 1, 1, 1, true, true, true, 1, true, true,
                new("growth-rate", "growth-rates/1"),
                [new()],
                [new("egg-group", "egg-groups/1")],
                new("color", "colors/1"),
                new("shape", "shapes/1"),
                new("evolves-from-species", "species/1"),
                new("evolves/1"),
                new("habitat", "habitats/1"),
                new("gen", "gens/1"),
                [new()],
                [new()],
                [new()],
                [new()],
                [new()],
                [new()]
            );
        }
        #endregion
    }
}
