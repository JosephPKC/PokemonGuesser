using PkmApi.DTOs.Pokemon;
using PkmApi.Endpoints;
using PkmApi.Utils;

namespace PkmApi
{
    internal class PkmApiManager : IPkmApi
    {
        private static readonly string _baseUri = "pokeapi.co/api";

        public PkmApiManager(string pVersion, IApiGetter? pApiGetter = null, IJsonParser? pJsonParser = null, ILogger? pLogger = null, ICacheFactory? pCacheFactory = null, int? pCacheSizeLimit = null, int? pCacheLifeInSec = null)
        {
            Pokemon = EndpointHandlerFactory.BuildEndpointHandler<PkmDTO>(_baseUri, pVersion, "pokemon", pApiGetter ?? new HttpGetter(), pJsonParser ?? new JsonParser(), pLogger ?? new ConsoleLogger(), pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        }

        #region IPkmApi
        public IEndpointHandler<PkmDTO> Pokemon { get; init; }
        #endregion
    }
}
