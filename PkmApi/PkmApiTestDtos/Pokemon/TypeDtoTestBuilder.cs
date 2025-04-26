using PkmApi.Dtos.Pokemon.Type;

namespace PkmApiTestDtos.Pokemon
{
    internal class TypeDtoTestBuilder : IDtoTestBuilder<TypeDto>
    {
        #region IDtoTestBuilder<TypeDto>
        public TypeDto GetBasic()
        {
            return new(1, "type");
        }

        public TypeDto GetEmpty()
        {
            return new(1, "type", new(), [], [], new("gen", "gens/1"), new("move-damage-class", "move-damage-classes/1"), [], [], []);
        }

        public TypeDto GetFull()
        {
            return new(
                1, "type", 
                new(
                    [new("no-damage-to", "types/1")],
                    [new("half-damage-to", "types/1")],
                    [new("double-damage-to", "types/1")],
                    [new("no-damage-from", "types/1")],
                    [new("half-damage-from", "types/1")],
                    [new("double-damage-from", "types/1")]
                ), 
                [new(
                    new("gen", "gens/1"),
                    new(
                        [new("no-damage-to-past", "types/1")],
                        [new("half-damage-to-past", "types/1")],
                        [new("double-damage-to-past", "types/1")],
                        [new("no-damage-from-past", "types/1")],
                        [new("half-damage-from-past", "types/1")],
                        [new("double-damage-from-past", "types/1")]
                    )
                )], 
                [new(1, new("gen", "gens/1"))], 
                new("gen", "gens/1"), 
                new("move-damage-class", "move-damage-classes/1"), 
                [new("name", new("language", "languages/1"))], 
                [new(1, new("type", "types/1"))], 
                [new("move", "moves/1")]
            );
        }

        public TypeDto GetShallow()
        {
            return new(
                1, "type",
                new(),
                [new()],
                [new()],
                new("gen", "gens/1"),
                new("move-damage-class", "move-damage-classes/1"),
                [new()],
                [new()],
                [new("move", "moves/1")]
            );
        }
        #endregion
    }
}
