using PkmApi.Dtos.Move.MoveLearnMethod;

namespace PkmApiTestDtos.Move
{
    public class MoveLearnMethodDtoTestBuilder : IDtoTestBuilder<MoveLearnMethodDto>
    {
        #region IDtoTestBuilder<MoveDamageClassDto>
        public MoveLearnMethodDto GetBasic()
        {
            return new(1, "move-learn-methods");
        }

        public MoveLearnMethodDto GetEmpty()
        {
            return new(1, "move-learn-methods", [], [], []);
        }

        public MoveLearnMethodDto GetFull()
        {
            return new(
                1, "move-learn-methods",
                [new("desc", new("language", "langauges/1"))],
                [new("name", new("language", "languages/1"))],
                [new("version-group", "version-groups/1")]
            );
        }

        public MoveLearnMethodDto GetShallow()
        {
            return new(
                1, "move-learn-methods",
                [new()],
                [new()],
                [new("version-group", "version-groups/1")]
            );
        }
        #endregion
    }
}
