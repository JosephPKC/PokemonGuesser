using PkmApi.Dtos.Game.VersionGroup;

namespace PkmApiTestDtos.Game
{
    public class VersionGroupDtoTestBuilder : IDtoTestBuilder<VersionGroupDto>
    {
        #region IDtoTestBuilder<VersionGroupDto>
        public VersionGroupDto GetBasic()
        {
            return new(1, "version-group");
        }

        public VersionGroupDto GetEmpty()
        {
            return new(1, "version-group", 1, new("gen", "gens/1"), [], [], [], []);
        }

        public VersionGroupDto GetFull()
        {
            return new(
                1,
                "version-group",
                1, 
                new("gen", "gens/1"),
                [new("learn-method", "learn-methods/1")],
                [new("pokedex", "pokedexes/1")],
                [new("region", "regions/1")],
                [new("version", "versions/1")]
            );
        }

        public VersionGroupDto GetShallow()
        {
            return new(
                1,
                "version-group",
                1,
                new("gen", "gens/1"),
                [new("learn-method", "learn-methods/1")],
                [new("pokedex", "pokedexes/1")],
                [new("region", "regions/1")],
                [new("version", "versions/1")]
            );
        }
        #endregion
    }
}
