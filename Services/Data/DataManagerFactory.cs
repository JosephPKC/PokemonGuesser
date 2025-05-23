using Data.PkmApi;
using Data.Utils;

namespace Data;
public static class DataManagerFactory
{
    public static IDataManager CreateDataManager(IPkmApiGateway pApi, ICacheHandlerFactory pCacheHandlerFactory, ILogFactory pLogFactory, int pCurrGenId)
    {
        return new DataManager(pApi, pCacheHandlerFactory, pLogFactory, pCurrGenId);
    }
}
