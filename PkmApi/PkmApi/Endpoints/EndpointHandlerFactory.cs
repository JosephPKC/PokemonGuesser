using PkmApi.DTOs;
using PkmApi.Utils;

namespace PkmApi.Endpoints
{
    public static class EndpointHandlerFactory
    {
        public static IEndpointHandler<TData> BuildEndpointHandler<TData>(string pBaseUri, string pVersion, string pEndpoint) where TData : BasePkmDTO
        {

            return BuildEndpointHandler<TData>(pBaseUri, pVersion, pEndpoint, new HttpGetter(), new JsonParser(), new ConsoleLogger());
        }

        public static IEndpointHandler<TData> BuildEndpointHandler<TData>(string pBaseUri, string pVersion, string pEndpoint, IApiGetter pApiGetter, IJsonParser pJsonParser, ILogger pLogger) where TData : BasePkmDTO
        {
            return new BasePkmEndpointHandler<TData>(pBaseUri, pVersion, pEndpoint, pApiGetter, pJsonParser, pLogger);
        }
    }
}
