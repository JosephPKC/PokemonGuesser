using StackExchange.Redis;

namespace PkmDataRetrieval.Utils.Cache.Redis
{
    public static class RedisHandlerFactory
    {
        public static ICacheHandler CreateNewRedisHandler(IConnectionMultiplexer pConnMulti, string pPrefixPath)
        {
            return new RedisDbHandler(pConnMulti, pPrefixPath);
        }
    }
}
