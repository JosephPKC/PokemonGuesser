using StackExchange.Redis;

using PkmDataRetrieval.Api;

namespace PkmDataRetrieval.Retrieval
{
    public static class DataRetrievalFactory
    {
        public static IDataRetrieval CreateDataRetriever(IPkmGateway pApi, IConnectionMultiplexer pConn, int pCurrGenId)
        {
            return new PkmDataRetriever(pApi, pConn, pCurrGenId);
        }
    }
}
