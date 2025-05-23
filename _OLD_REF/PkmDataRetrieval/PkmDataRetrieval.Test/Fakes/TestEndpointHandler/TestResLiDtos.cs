using PkmApi.Dtos.Utility;

namespace PkmDataRetrieval.Test.Fakes.TestEndpointHandler
{
    internal static class TestResLiDtos
    {
        public static ResLiDto GetResLiDto()
        {
            return new()
            {
                Count = 1,
                Next = "test-next",
                Previous = "test-prev",
                Results = [
                    new NamedApiResDto(
                        "test-name-dto-1",
                        "test-url-1"
                    )
                ]
            };
        }

        public static ResLiDto GetResLiDtoWithNullResults()
        {
            return new()
            {
                Count = 1,
                Next = "test-next",
                Previous = "test-prev",
                Results = null
            };
        }

        public static ResLiDto GetResLiDtoWithNullCount()
        {
            return new()
            {
                Count = null,
                Next = "test-next",
                Previous = "test-prev",
                Results = []
            };
        }

        public static ResLiDto GetResLiDtoWithZeroCount()
        {
            return new()
            {
                Count = 0,
                Next = "test-next",
                Previous = "test-prev",
                Results = []
            };
        }
    }
}
