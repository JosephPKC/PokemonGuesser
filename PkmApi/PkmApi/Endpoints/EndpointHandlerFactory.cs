using PkmApi.Dtos;
using PkmApi.Utils;

namespace PkmApi.Endpoints
{
    public static class EndpointHandlerFactory
    {
        public static IEndpointHandler<TData> BuildEndpointHandler<TData>(string pBaseUri, string pVersion, string pEndpoint) where TData : class, IPkmApiDto
        {

            return BuildEndpointHandler<TData>(pBaseUri, pVersion, pEndpoint, new HttpGetter(), new JsonParser(), new ConsoleLogger(), new CacheFactory(), Config.DefaultCacheSizeLimit, Config.DefaultCacheLifeInSec);
        }

        public static IEndpointHandler<TData> BuildEndpointHandler<TData>(
            string pBaseUri, string pVersion, string pEndpoint, 
            IApiGetter pApiGetter, IJsonParser pJsonParser, ILogger pLogger, 
            ICacheFactory? pCacheFactory = null, int? pCacheSizeLimit = null, int? pCacheLifeInSec = null
            ) where TData : class, IPkmApiDto
        {
            return new BasePkmEndpointHandler<TData>(pBaseUri, pVersion, pEndpoint, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        }
    }
}
