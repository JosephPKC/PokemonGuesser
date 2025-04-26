using PkmApi.Dtos.Move.MoveDamageClass;

namespace PkmApiTestDtos.Move
{
    public class MoveDamageClassDtoTestBuilder : IDtoTestBuilder<MoveDamageClassDto>
    {
        #region IDtoTestBuilder<MoveDamageClassDto>
        public MoveDamageClassDto GetBasic()
        {
            return new(1, "move-damage-class");
        }

        public MoveDamageClassDto GetEmpty()
        {
            return new(1, "move-damage-class", [], [], []);
        }

        public MoveDamageClassDto GetFull()
        {
            return new(
                1, "move-damage-class",
                [new("desc", new("language", "langauges/1"))],
                [new("move", "moves/1")],
                [new("name", new("language", "languages/1"))]
            );
        }

        public MoveDamageClassDto GetShallow()
        {
            return new(
                1, "move-damage-class",
                [new()],
                [new("move", "moves/1")],
                [new()]
            );
        }
        #endregion
    }
}
