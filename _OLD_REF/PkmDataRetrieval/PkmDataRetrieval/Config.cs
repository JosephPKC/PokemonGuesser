namespace PkmDataRetrieval
{
    internal static class Config
    {
        #region Cache Configs
        public const string RedisConnect = "localhost:6379";
        public const string RedisKubeConnect = "redis-serv.pkm-guess.svc.cluster.local:6379";

        public const string ServiceKeyPrefix = "pkm-data-retrieval";
        public const string RetKeyPrefix = "ret-models";
        public const string GenByIdKeyPrefix = "gen-by-id";
        public const string PkmAllKeyPrefix = "pkm-all";
        public const string PkmByIdKeyPrefix = "pkm-by-id";
        #endregion

        #region Api Configs
        public const string CurrentApiVers = "v2";

        public const int CurrentGenId = 9;
        public const int EngLangId = 9;
        public const int MachineLearnMethodId = 3;

        public const string LangResName = "language";
        public const string LevelLearnMethodNameKey = "level-up";
        public const string MachineLearnMethodNameKey = "machine";
        #endregion

        #region Defaults
        public const string DefaultMachineName = "TM00";
        #endregion
    }
}
