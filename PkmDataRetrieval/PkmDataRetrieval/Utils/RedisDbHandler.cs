using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

namespace PkmDataRetrieval.Utils
{
    //  A basic handler that makes common usage easier.
    public class RedisDbHandler(IConnectionMultiplexer pRedisMutex, string pPrefixPath)
    {
        private readonly IDatabase _db = pRedisMutex.GetDatabase();
        private readonly string _prefixPath = pPrefixPath;

        public bool Add<TData>(string pKey, TData pData, int? pLifeInSec = null, bool pOverwrite = false) where TData : class
        {
            if (string.IsNullOrWhiteSpace(pKey))
            {
                return false;
            }

            string key = $"{_prefixPath}:{pKey}";

            TData? item = Get<TData>(key);
            if (!EqualityComparer<TData>.Default.Equals(item, default) && !pOverwrite)
            {
                return false;
            }

            bool result = _db.JSON().Set(key, "$", pData);
            if (!result)
            {
                return false;
            }

            if (pLifeInSec == null)
            {
                return result;
            }

            return _db.KeyExpire(pKey, TimeSpan.FromSeconds(pLifeInSec.Value));
        }

        public TData? Get<TData>(string pKey) where TData : class
        {
            if (string.IsNullOrWhiteSpace(pKey))
            {
                return default;
            }

            string key = $"{_prefixPath}:{pKey}";

            return _db.JSON().Get<TData>(key);
        }
    }
}
