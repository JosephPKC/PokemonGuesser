namespace PkmApi.Utils
{
    public class HttpGetter : IApiGetter
    {
        private readonly HttpClient _client;

        public HttpGetter() => _client = new HttpClient();
        public HttpGetter(HttpClient pClient) => _client = pClient;

        #region IApiGetter
        public string Get(string pUri)
        {
            return GetAsync(pUri).GetAwaiter().GetResult();
        }

        public async Task<string> GetAsync(string pUri)
        {
            using HttpResponseMessage resp = await _client.GetAsync(pUri);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsStringAsync();
        }
        #endregion
    }
}
