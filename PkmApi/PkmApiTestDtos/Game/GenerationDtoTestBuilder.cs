using PkmApi.Dtos.Game.Generation;

namespace PkmApiTestDtos.Game
{
    public class GenerationDtoTestBuilder : IDtoTestBuilder<GenerationDto>
    {
        #region IDtoTestBuilder<GenerationDto>
        public GenerationDto GetBasic()
        {
            return new(1, "gen");
        }

        public GenerationDto GetEmpty()
        {
            return new(1, "gen", [], [], new("region", "regions/1"), [], [], [], []);
        }

        public GenerationDto GetFull()
        {
            return new(
                1,
                "gen",
                [new("ability", "ability/1")],
                [new("name", new("language", "languages/1"))],
                new("region", "regions/1"),
                [new("move", "moves/1")],
                [new("species", "species/1")],
                [new("type", "types/1")],
                [new("version-group", "version-groups/1")]
            );
        }

        public GenerationDto GetShallow()
        {
            return new(
                1,
                "gen",
                [new("ability", "ability/1")],
                [new()],
                new("region", "regions/1"),
                [new("move", "moves/1")],
                [new("species", "species/1")],
                [new("type", "types/1")],
                [new("version-group", "version-groups/1")]
            );
        }
        #endregion
    }
}
