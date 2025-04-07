namespace PkmApi
{
    internal static class Config
    {
        public const int DefaultCacheSizeLimit = 1024;
        public const int DefaultCacheLifeInSec = 600;

        public const int DefaultApiPaginationLimit = 20;
        public const int DefaultApiPaginationOffset = 0;
        public const string DefaultApiVersion = "v2";
        public const string ApiBaseUri = "pokeapi.co/api";
    }
}
