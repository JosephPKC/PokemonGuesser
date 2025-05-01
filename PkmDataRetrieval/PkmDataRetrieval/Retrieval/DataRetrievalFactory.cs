using PkmDataRetrieval.Api;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval
{
    public static class DataRetrievalFactory
    {
        public static IDataRetrieval CreateDataRetriever(IPkmGateway pApi, ICacheHandler pCache, int pCurrGenId)
        {
            return new PkmDataRetriever(pApi, pCache, pCurrGenId);
        }
    }
}
