using System.Text.Json;

namespace PkmApi.Utils
{
    // TODO: We want to eventually use the UtilsLab version, once we get a NuGet package set up going for that.
    // But, for now, we can just use this.
    internal class JsonParser : IJsonParser
    {
        #region IJsonParser
        public TObj? Deserialize<TObj>(string pJson)
        {
            return JsonSerializer.Deserialize<TObj>(pJson);
        }

        public IEnumerable<TObj> DeserializeArray<TObj>(string pJson)
        {
            return JsonSerializer.Deserialize<IEnumerable<TObj>>(pJson) ?? [];
        }
        #endregion
    }
}
