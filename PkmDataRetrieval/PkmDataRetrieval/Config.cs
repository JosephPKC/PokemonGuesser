namespace PkmDataRetrieval
{
    internal static class Config
    {
        public const string RedisConnect = "localhost:6379";

        public const string RedisPkmKeyPrefix = "pkm-data-retrieval:pkm-dto";
        public const int DefaultRedisPkmKeyLifeInSec = 18000;   //  30 minutes
    }
}
