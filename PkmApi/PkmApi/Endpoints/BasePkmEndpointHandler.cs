using System;
using PkmApi.DTOs;
using PkmApi.DTOs.Shared;
using PkmApi.Utils;

namespace PkmApi.Endpoints
{
    internal class BasePkmEndpointHandler<TData>(
        string pBaseUri, string pVersion, string pEndpoint, IApiGetter pApiGetter, IJsonParser pJsonParser, ILogger pLogger, 
        ICacheFactory? pCacheFactory = null, int? pCacheSizeLimit = null, int? pCacheLifeInSec = null) 
        : IEndpointHandler<TData> where TData : IPkmApiDTO
    {
        private readonly string _baseUri = pBaseUri;
        private readonly string _version = pVersion;
        private readonly string _endpoint = pEndpoint;

        private readonly IApiGetter _apiGetter = pApiGetter;
        private readonly IJsonParser _jsonParser = pJsonParser;
        private readonly ILogger log = pLogger;
        private readonly ICache<IPkmApiDTO>? _cache = pCacheFactory == null ? null : pCacheFactory.BuildCache<IPkmApiDTO>(pCacheSizeLimit, pCacheLifeInSec);

        #region IEndpointHandler<TData>
        public TData? GetById(string pId)
        {
            string uri = BuildUri(_baseUri, _version, _endpoint, pId);
            log.Info($"BEGIN: GetById: {uri}");

            TData? result = GetData<TData>(uri);

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

            ResLiDTO? result = GetData<ResLiDTO>(uri);

            log.Info($"END: GetAll: {uri}");

            return result;
        }
        #endregion

        private TActual? GetData<TActual>(string pUri) where TActual : IPkmApiDTO
        {
            if (_cache != null)
            {
                //  Try to get from cache first
                IPkmApiDTO? fromCache = _cache.Get(pUri);
                if (fromCache != null)
                {
                    log.Debug("Found in cache.");
                    return (TActual)fromCache;
                }
            }

            //  Cache is disabled, or could not find in cache.
            string json = _apiGetter.Get(pUri);
            log.Debug($"Received JSON: {json}");

            TActual? result = _jsonParser.Deserialize<TActual>(json);
            if (result == null)
            {
                return default;
            }

            if (_cache != null)
            {
                log.Debug($"Added to cache with key {pUri}.");
                _cache.Add(pUri, result);
            }

            return result;
        }

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
