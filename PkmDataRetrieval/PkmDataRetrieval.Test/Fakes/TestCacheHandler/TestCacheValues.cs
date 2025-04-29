using PkmDataRetrieval.Api.Models.Pokemon;

namespace PkmDataRetrieval.Test.Fakes.TestCacheHandler
{
    internal static class TestCacheValues
    {
        public static PkmAllModel GetPkmAllModel()
        {
            return new()
            {
                Ids = [1, 2, 3]
            };
        }
    }
}
