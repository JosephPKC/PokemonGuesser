using PkmApi.DTOs;
using PkmApi.DTOs.Shared;
using PkmApi.Utils;

namespace PkmApi.Endpoints
{
    internal class BasePkmEndpointHandler<TData>(string pBaseUri, string pVersion, string pEndpoint, IApiGetter pApiGetter, IJsonParser pJsonParser, ILogger pLogger) : IEndpointHandler<TData> where TData : BasePkmDTO
    {
        private readonly string _baseUri = pBaseUri;
        private readonly string _version = pVersion;
        private readonly string _endpoint = pEndpoint;

        private readonly IApiGetter _apiGetter = pApiGetter;
        private readonly IJsonParser _jsonParser = pJsonParser;
        private readonly ILogger log = pLogger;

        #region IEndpointHandler<TData>
        public TData? GetById(string pId)
        {
            string uri = BuildUri(_baseUri, _version, _endpoint, pId);
            log.Info($"BEGIN: GetById: {uri}");

            string json = _apiGetter.Get(uri);
            log.Debug($"Received JSON: {json}");

            TData? result = _jsonParser.Deserialize<TData>(json);

            log.Info($"END: GetById: {uri}");

            return result;
        }

        public ResLiDTO? GetAll(int pLimit = 20, int pOffset = 0)
        {
            IDictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "limit", pLimit.ToString() }, 
                { "offset", pOffset.ToString() }
            };
            string uri = BuildUri(_baseUri, _version, _endpoint, null, queryParams);
            log.Info($"BEGIN: GetAll: {uri}");

            string json = _apiGetter.Get(uri);
            log.Debug($"Received JSON: {json}");

            ResLiDTO? result = _jsonParser.Deserialize<ResLiDTO>(json);

            log.Info($"END: GetAll: {uri}");

            return result;
        }
        #endregion

        private static string BuildUri(string pBaseUri, string pVersion, string pEndpoint, string? pId = null, IDictionary<string, string>? pQueryParams = null)
        {
            Utils.UriBuilder uriBuilder = new();
            uriBuilder.SetHttps(true).SetBaseUri(pBaseUri).SetVersion(pVersion).AddEndpoint(pEndpoint, pId);

            if (pQueryParams != null)
            {
                uriBuilder.AddQueryParams(pQueryParams);
            }

            return uriBuilder.BuildUri() ?? string.Empty;
        }
    }
}
