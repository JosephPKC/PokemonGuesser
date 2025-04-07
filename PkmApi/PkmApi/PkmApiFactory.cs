using PkmApi.Utils;

namespace PkmApi
{
    public static class PkmApiFactory
    {
        public static IPkmApi CreatePkmApi(string pVersion = "v2", IApiGetter? pApiGetter = null, IJsonParser? pJsonParser = null, ILogger? pLogger = null, ICacheFactory? pCacheFactory = null, int? pCacheSizeLimit = null, int? pCacheLifeInSec = null)
        {
            return new PkmApiManager(pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        }
    }
}
