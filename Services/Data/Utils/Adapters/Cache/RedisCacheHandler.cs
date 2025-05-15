using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

namespace Data.Utils.Adapters.Cache;
internal class RedisCacheHandler(IConnectionMultiplexer pConnMulti, string pPrefixPath) : ICacheHandler
{
    private readonly IDatabase _db = pConnMulti.GetDatabase();
    private readonly RedisKeyPrefixer _keyPrefixer = new(pPrefixPath);

    #region ICacheHandler
    public bool Add<TItem>(string pKey, TItem pData, int? pLifeInSec = null, bool pOverwrite = false) where TItem : class
    {
        if (string.IsNullOrWhiteSpace(pKey))
        {
            return false;
        }

        string key = _keyPrefixer.GetKey<TItem>(pKey);

        TItem? item = Get<TItem>(key);
        if (!EqualityComparer<TItem>.Default.Equals(item, default) && !pOverwrite)
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

    public TItem? Get<TItem>(string pKey) where TItem : class
    {
        if (string.IsNullOrWhiteSpace(pKey))
        {
            return default;
        }

        string key = _keyPrefixer.GetKey<TItem>(pKey);

        return _db.JSON().Get<TItem>(key);
    }
    #endregion

}
