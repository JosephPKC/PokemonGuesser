using PkmApi.Dtos.Game.Version;

namespace PkmApiTestDtos.Game
{
    public class VersionDtoTestBuilder : IDtoTestBuilder<VersionDto>
    {
        #region IDtoTestBuilder<VersionDto>
        public VersionDto GetBasic()
        {
            return new(1, "version");
        }

        public VersionDto GetEmpty()
        {
            return new(1, "version", [], new("version-group", "version-groups/1"));
        }

        public VersionDto GetFull()
        {
            return new(
                1,
                "version",
                [new("names", new("language", "languages/1"))],
                new("version-group", "version-groups/1")
            );
        }

        public VersionDto GetShallow()
        {
            return new(
                1,
                "version",
                [new()],
                new("version-group", "version-groups/1")
            );
        }
        #endregion
    }
}
