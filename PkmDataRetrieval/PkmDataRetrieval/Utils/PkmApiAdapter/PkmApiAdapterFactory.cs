using StackExchange.Redis;

namespace PkmDataRetrieval.Utils.PkmApiAdapter
{
    public static class PkmApiAdapterFactory
    {
        public static IPkmApi CreatePkmApiAdapter(IConnectionMultiplexer pMutex)
        {
            return new PkmApiAdapter(pMutex);
        }
    }
}
