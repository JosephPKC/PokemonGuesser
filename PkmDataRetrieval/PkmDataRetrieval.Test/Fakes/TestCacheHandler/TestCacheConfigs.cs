using PkmDataRetrieval.Api.Models.Pokemon;
using PkmDataRetrieval.Retrieval.Models;

namespace PkmDataRetrieval.Test.Fakes.TestCacheHandler
{
    internal class TestCacheConfigs
    {
        public PkmAllModel? ReturnThisPkmAllModel { get; set; } = null;

        public Dictionary<Type, BaseRetModel> ReturnThisRet { get; set; } = [];
    }
}
