using PkmApi.DTOs;
using PkmApi.Utils;

namespace PkmApi.Endpoints
{
    public static class EndpointHandlerFactory
    {
        public static IEndpointHandler<TData> BuildEndpointHandler<TData>(string pBaseUri, string pVersion, string pEndpoint) where TData : IPkmApiDTO
        {

            return BuildEndpointHandler<TData>(pBaseUri, pVersion, pEndpoint, new HttpGetter(), new JsonParser(), new ConsoleLogger(), new CacheFactory(), 1024, 600);
        }

        public static IEndpointHandler<TData> BuildEndpointHandler<TData>(
            string pBaseUri, string pVersion, string pEndpoint, 
            IApiGetter pApiGetter, IJsonParser pJsonParser, ILogger pLogger, 
            ICacheFactory? pCacheFactory = null, int? pCacheSizeLimit = null, int? pCacheLifeInSec = null
            ) where TData : IPkmApiDTO
        {
            return new BasePkmEndpointHandler<TData>(pBaseUri, pVersion, pEndpoint, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        }
    }
}
