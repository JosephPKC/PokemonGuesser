using PkmDataRetrieval.Api.Models.Pokemon;
using PkmDataRetrieval.Retrieval.Models;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Test.Fakes.TestCacheHandler
{
    internal class TestCacheHandler(TestCacheConfigs? pConfigs = null) : ICacheHandler
    {
        public TestCacheConfigs Configs { get; set; } = pConfigs ?? new();
        #region ICacheHandler
        public bool Add<TData>(string pKey, TData pData, int? pLifeInSec = null, bool pOverwrite = false) where TData : class
        {
            return true;
        }

        public TData? Get<TData>(string pKey) where TData : class
        {
            string typeName = typeof(TData).Name;

            if (typeName.Contains("Ret"))
            {
                return GetTestRet<TData>();
            }

            return typeof(TData).Name switch
            {
                nameof(PkmAllModel) => Configs.ReturnThisPkmAllModel as TData,
                _ => null
            };
        }
        #endregion

        private TData? GetTestRet<TData>() where TData : class
        {
            Type retType = typeof(TData);
            if(Configs.ReturnThisRet.TryGetValue(retType, out BaseRetModel? value))
            {
                return value as TData;
            }

            return null;
        }
    }
}
