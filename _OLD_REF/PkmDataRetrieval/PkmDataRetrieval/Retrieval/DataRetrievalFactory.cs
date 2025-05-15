using PkmDataRetrieval.Api;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval
{
    public static class DataRetrievalFactory
    {
        public static IDataRetrieval CreateDataRetriever(IPkmGateway pApi, ICacheHandler pCache, LogWrapper.Loggers.ILoggerFactory pLoggerFactory, int pCurrGenId)
        {
            return new PkmDataRetriever(pApi, pCache, pLoggerFactory, pCurrGenId);
        }
    }
}
