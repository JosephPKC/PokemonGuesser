using PkmDataRetrieval.Retrieval.Models;
using PkmDataRetrieval.Retrieval.Models.Generation;
using PkmDataRetrieval.Retrieval.Models.VersionGroup;

namespace PkmDataRetrieval.Test.Fakes.TestValues
{
    internal static class TestRets
    {
        public static IEnumerable<BasicRetModel> TestBasicRetList = [
            new() {
                ResUrl = "test-url-1"
            }
        ];

        public static GenerationRetModel GetTestGenRet()
        {
            return new()
            {
                Id = 1,
                NameKey = "gen-key",
                Names = { { "en-lang", "name" } },
                VersionGroupResUrls = ["gen/1", "gen/2"]
            };
        }

    }
}
