using PkmApi.DTOs.Pokemon;
using PkmApi.Endpoints;
using PkmApi.Utils;

namespace PkmApi
{
    public class PkmApiManager
    {
        private static readonly string _baseUri = "pokeapi.co/api";

        public PkmApiManager()
        {
            Pokemon = EndpointHandlerFactory.BuildEndpointHandler<PkmDTO>(_baseUri, "v2", "pokemon", new HttpGetter(), new JsonParser(), new ConsoleLogger());
        }

        public PkmApiManager(string pVersion, IApiGetter? pApiGetter = null, IJsonParser? pJsonParser = null, ILogger? pLogger = null)
        {
            Pokemon = EndpointHandlerFactory.BuildEndpointHandler<PkmDTO>(_baseUri, pVersion, "pokemon", pApiGetter ?? new HttpGetter(), pJsonParser ?? new JsonParser(), pLogger ?? new ConsoleLogger());
        }

        #region Endpoints
        public IEndpointHandler<PkmDTO> Pokemon { get; init; }
        #endregion
    }
}
