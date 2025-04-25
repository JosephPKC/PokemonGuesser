using PkmDataRetrieval.Retrieval.Models;

namespace PkmDataRetrieval.Test.Fakes.TestValues
{
    internal static class TestRets
    {
        public static IEnumerable<BasicRetModel> TestBasicRetList = [
            new() {
                ResUrl = "test-url-1"
            }
        ];
    }
}
