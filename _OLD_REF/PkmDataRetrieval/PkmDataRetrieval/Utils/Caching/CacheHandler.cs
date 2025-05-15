using RedisCache;

namespace PkmDataRetrieval.Utils.Caching
{
    public class CacheHandler(IRedisHandler pRedis) : ICacheHandler
    {
        private readonly IRedisHandler _redis = pRedis;

        #region ICacheHandler
        public bool Add<TData>(string pKey, TData pData, int? pLifeInSec = null, bool pOverwrite = false) where TData : class
        {
            return _redis.Add(pKey, pData, pLifeInSec, pOverwrite);
        }

        public TData? Get<TData>(string pKey) where TData : class
        {
            return _redis.Get<TData>(pKey);
        }
        #endregion
    }
}
