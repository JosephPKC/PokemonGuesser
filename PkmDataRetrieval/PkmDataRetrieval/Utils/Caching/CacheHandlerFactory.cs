using RedisCache;

namespace PkmDataRetrieval.Utils.Caching
{
    public static class CacheHandlerFactory
    {
        public static ICacheHandler CreateNewCacheHandler(IRedisHandler pRedis)
        {
            return new CacheHandler(pRedis);
        }
    }
}
