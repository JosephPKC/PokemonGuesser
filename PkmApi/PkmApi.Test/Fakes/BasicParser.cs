using System.Text.Json;

using PkmApi.Utils;

namespace PkmApi.Test.Fakes
{
    internal class BasicParser : IJsonParser
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
