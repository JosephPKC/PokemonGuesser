using PkmApi.Dtos.Utility;

namespace PkmApiTestDtos.Utility
{
    public class ResLiDtoTestBuilder : IDtoTestBuilder<ResLiDto>
    {
        #region IDtoTestBuilder<ResLiDto>
        public ResLiDto GetBasic()
        {
            return new();
        }

        public ResLiDto GetEmpty()
        {
            return new(1, "next", "previous", []);
        }

        public ResLiDto GetFull()
        {
            return new(
                1, "next", "previous", 
                [new("res", "res/1")]
            );
        }

        public ResLiDto GetShallow()
        {
            return new(
                1, "next", "previous",
                [new("res", "res/1")]
            );
        }
        #endregion
    }
}
