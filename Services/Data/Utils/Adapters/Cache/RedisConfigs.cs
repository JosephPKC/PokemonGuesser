namespace Data.Utils.Adapters.Cache;
internal static class RedisConfigs
{
    public const string RedisConnect = "localhost:6379";
    public const string RedisKubeConnect = "redis-serv.pkm-guess.svc.cluster.local:6379";

    public const string ServiceKeyPrefix = "pkm-data";
    public const string DataModelKeyPrefix = "data-models";
}
