using PkmApi.DTOs.Pokemon;
using PkmApi.Endpoints;
using PkmApi.Utils;

namespace PkmApi
{
    internal class PkmApiManager(string pVersion, 
        IApiGetter? pApiGetter = null, IJsonParser? pJsonParser = null, ILogger? pLogger = null, 
        ICacheFactory? pCacheFactory = null, int? pCacheSizeLimit = null, int? pCacheLifeInSec = null) 
        : IPkmApi
    {
        private static readonly string _baseUri = "pokeapi.co/api";

        #region IPkmApi
        public IEndpointHandler<PkmDTO> Pokemon { get; init; } = EndpointHandlerFactory.BuildEndpointHandler<PkmDTO>(
            _baseUri, pVersion, "pokemon", 
            pApiGetter ?? new HttpGetter(), pJsonParser ?? new JsonParser(), pLogger ?? new ConsoleLogger(), 
            pCacheFactory, pCacheSizeLimit, pCacheLifeInSec
        );
        #endregion
    }
}
