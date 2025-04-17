using System.Collections.Immutable;

namespace PkmDataRetrieval
{
    internal static class Config
    {
        #region Redis Configs
        public const string RedisConnect = "localhost:6379";
        public const string RedisServiceKeyPrefix = "pkm-data-retrieval";
        public const string RedisRetKeyPrefix = "ret-models";
        public const string RedisGenByIdKeyPrefix = "gen-by-id";
        public const string RedisPkmAllKeyPrefix = "pkm-all";
        public const string RedisPkmByIdKeyPrefix = "pkm-by-id";
        #endregion

        #region Api Configs
        public const string CurrentApiVers = "v2";
        public const int CurrentGenId = 9;
        public const int EngLangId = 9;
        #endregion


    }
}
