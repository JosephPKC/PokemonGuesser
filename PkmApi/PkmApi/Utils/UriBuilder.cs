using System.Text;

namespace PkmApi.Utils
{
    internal class UriBuilder
    {
        private bool _isHttps = true;
        private string _baseUri = string.Empty;
        private string _version = string.Empty;
        private readonly Dictionary<string, string> _endpointPairs = [];
        private readonly Dictionary<string, string> _queryParams = [];

        public UriBuilder SetHttps(bool pIsHttps)
        {
            _isHttps = pIsHttps;
            return this;
        }

        public UriBuilder SetBaseUri(string pBaseUri)
        {
            _baseUri = pBaseUri;
            return this;
        }

        public UriBuilder SetVersion(string pVersion)
        {
            _version = pVersion;
            return this;
        }

        public UriBuilder AddEndpoint(string? pEndpoint = "", string? pId = "")
        {
            string?[] endpointParams = [pEndpoint, pId];
            if (endpointParams.All(string.IsNullOrWhiteSpace))
            {
                //  Do not add if both endpoint and id are blank or null.
                return this;
            }

            _endpointPairs.Add(pEndpoint ?? "", pId ?? "");
            return this;
        }

        public UriBuilder AddEndpoints(IDictionary<string?, string?> pEndpoints) {
            foreach (KeyValuePair<string?, string?> endpoint in pEndpoints)
            {
                AddEndpoint(endpoint.Key, endpoint.Value);
            }
            return this;
        }

        public UriBuilder AddQueryParam(string pQueryKey, string pQueryVal)
        {
            _queryParams.Add(pQueryKey, pQueryVal);
            return this;
        }

        public UriBuilder AddQueryParams(IDictionary<string, string> pQueryParams) {
            foreach (KeyValuePair<string, string> queryParam in pQueryParams)
            {
                AddQueryParam(queryParam.Key, queryParam.Value);
            }
            return this;
        }

        public string? BuildUri()
        {
            //  BaseUri/Version/Endpoint/Id?QueryParams
            if (string.IsNullOrWhiteSpace(_baseUri))
            {
                return null;
            }

            StringBuilder strBuilder = new();
            //  Build out the base uri.
            strBuilder.Append(_isHttps ? "https://" : "http://").Append(_baseUri);

            if (!string.IsNullOrWhiteSpace(_version))
            {
                strBuilder.Append($"/{_version}");
            }

            // Build out the endpoint resource/id pairs
            foreach (KeyValuePair<string, string> endpointPair in _endpointPairs)
            {
                //  Blank endpoints or ids (or both) are allowed.
                //  If both are blank, then it is skipped.
                if (!string.IsNullOrWhiteSpace(endpointPair.Key))
                {
                    strBuilder.Append($"/{endpointPair.Key}");
                }
                
                if (!string.IsNullOrWhiteSpace(endpointPair.Value))
                {
                    strBuilder.Append($"/{endpointPair.Value}");
                }
            }

            //  Build out the query params.
            bool isFirstQueryParam = true;
            foreach (KeyValuePair<string, string> queryParam in _queryParams)
            {
                if (string.IsNullOrWhiteSpace(queryParam.Key) || string.IsNullOrWhiteSpace(queryParam.Value))
                {
                    //  A QueryParam requires both the key and value.
                    continue;
                }

                if (isFirstQueryParam)
                {
                    strBuilder.Append($"?{queryParam.Key}={queryParam.Value}");
                    isFirstQueryParam = false;
                }
                else
                {
                    strBuilder.Append($"&{queryParam.Key}={queryParam.Value}");
                }
            }

            return strBuilder.ToString();
        }
    }
}
