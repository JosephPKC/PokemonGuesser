using PkmDataRetrieval.Api.Models.Pokemon;

namespace PkmDataRetrieval.Retrieval.Builders
{
    internal static class PkmAllModelBuilder
    {
        public static PkmAllModel BuildModel(IEnumerable<int> pIds)
        {
            return new()
            {
                Ids = pIds
            };
        }
    }
}
