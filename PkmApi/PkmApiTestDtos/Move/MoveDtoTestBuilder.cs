using PkmApi.Dtos.Move.Move;

namespace PkmApiTestDtos.Move
{
    public class MoveDtoTestBuilder : IDtoTestBuilder<MoveDto>
    {
        #region IDtoTestBuilder<MoveDto>
        public MoveDto GetBasic()
        {
            return new(1, "move");
        }

        public MoveDto GetEmpty()
        {
            return new(
                1, "move", 10, 10, 10, 10, 10, new(), 
                new("contest-type", "contest-types/1"), new("contest-effects/1"), new("damage-class", "damage-classes/1"), 
                [], [], [], [], new("gen", "gens/1"), [], new(), [], [], [], new("super-contest-effects/1"), 
                new("target", "targets/1"), new("type", "types/1")
            );
        }

        public MoveDto GetFull()
        {
            return new(
                1, "move", 10, 10, 10, 10, 10, 
                new(
                    new(
                        [new("normal-use-before", "use-befores/1")], 
                        [new("normal-use-after", "use-afters/1")]
                    ),
                    new(
                        [new("super-use-before", "use-befores/1")], 
                        [new("super-use-after", "use-afters/1")]
                    )
                ),
                new("contest-type", "contest-types/1"), 
                new("contest-effects/1"), 
                new("damage-class", "damage-classes/1"),
                [new("effect", "short-effect", new("language", "languages/1"))], 
                [new(
                    [new("effect", new("language", "languages/1"))],
                    new("version-group", "version-groups/1")
                )], 
                [new("learned-by-pkm", "pkm/1")], 
                [new("flavor-text", new("language", "languages/1"), new("version-group", "version-groups/1"))], 
                new("gen", "gens/1"), 
                [new(new("machines/1"), new("version-group", "version-groups/1"))], 
                new(
                    new("ailment", "ailments/1"),
                    new("category", "categories/1"),
                    10, 10, 10, 10, 10, 10, 10, 10, 10, 10
                ),
                [new("name", new("language", "languages/1"))], 
                [new(
                    5, 5, 5, 5,
                    [new("effect", "short-effect", new("language", "languages/1"))],
                    new("type", "types/1"),
                    new("version-group", "version-groups/1")
                )], 
                [new(5, new("stat", "stats/1"))], 
                new("super-contest-effects/1"),
                new("target", "targets/1"), 
                new("type", "types/1")
            );
        }

        public MoveDto GetShallow()
        {
            return new(
                1, "move", 10, 10, 10, 10, 10,
                new(),
                new("contest-type", "contest-types/1"),
                new("contest-effects/1"),
                new("damage-class", "damage-classes/1"),
                [new()],
                [new()],
                [new("learned-by-pkm", "pkm/1")],
                [new()],
                new("gen", "gens/1"),
                [new()],
                new(),
                [new()],
                [new()],
                [new()],
                new("super-contest-effects/1"),
                new("target", "targets/1"),
                new("type", "types/1")
            );
        }
        #endregion
    }
}
