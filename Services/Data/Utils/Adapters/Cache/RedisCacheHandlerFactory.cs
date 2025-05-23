using StackExchange.Redis;

namespace Data.Utils.Adapters.Cache;
public class RedisCacheHandlerFactory : ICacheHandlerFactory
{
    #region ICacheHandlerFactory
    public ICacheHandler CreateCacheHandler()
    {
        IConnectionMultiplexer connMulti = ConnectionMultiplexer.Connect(
            RedisConfigs.RedisConnect, // Change to kube when kube is set up
            config => config.AbortOnConnectFail = false
        );

        return new RedisCacheHandler(connMulti, RedisConfigs.ServiceKeyPrefix);
    }
    #endregion
}
