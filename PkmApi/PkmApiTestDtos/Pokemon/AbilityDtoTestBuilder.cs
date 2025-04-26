using PkmApi.Dtos.Pokemon.Ability;

namespace PkmApiTestDtos.Pokemon
{
    internal class AbilityDtoTestBuilder : IDtoTestBuilder<AbilityDto>
    {
        #region IDtoTestBuilder<AbilityDto>
        public AbilityDto GetBasic()
        {
            return new(1, "ability");
        }

        public AbilityDto GetEmpty()
        {
            return new(1, "ability", true, new("gen", "gens/1"), [], [], [], [], []);
        }

        public AbilityDto GetFull()
        {
            return new(
                1, "ability", true,
                new("gen", "gens/1"),
                [new("name", new("language", "languages/1"))],
                [new("effect", "short-effect", new("language", "languages/1"))],
                [new(
                    [new("effect", new("language", "langauges/1"))], 
                    new("version-group", "version-groups/1")
                )],
                [new(
                    "flavor-text", 
                    new("language", "languages/1"), 
                    new("version-group", "version-groups/1")
                )],
                [new(true, 1)]
            );
        }

        public AbilityDto GetShallow()
        {
            return new(
                1, "ability", true,
                new("gen", "gens/1"),
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
