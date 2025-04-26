using PkmApi.Dtos.Game.Pokedex;

namespace PkmApiTestDtos.Game
{
    public class PokedexDtoTestBuilder : IDtoTestBuilder<PokedexDto>
    {
        #region IDtoTestBuilder<GenerationDto>
        public PokedexDto GetBasic()
        {
            return new(1, "pokedex");
        }

        public PokedexDto GetEmpty()
        {
            return new(1, "pokedex", true, [], [], [], new("region", "regions/1"), []);
        }

        public PokedexDto GetFull()
        {
            return new(
                1,
                "pokedex",
                true,
                [new("desc", new("language", "languages/1"))],
                [new("name", new("name", "names/1"))],
                [new(1, new("species", "species/1"))],
                new("region", "regions/1"),
                [new("version-group", "version-groups/1")]
            );
        }

        public PokedexDto GetShallow()
        {
            return new(
                1,
                "pokedex",
                true,
                [new()],
                [new()],
                [new()],
                new("region", "regions/1"),
                [new("version-group", "version-groups/1")]
            );
        }
        #endregion
    }
}
