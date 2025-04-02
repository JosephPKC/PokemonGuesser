namespace PkmApi.Utils
{
    public interface IJsonParser
    {
        TObj? Deserialize<TObj>(string pJson);
        IEnumerable<TObj> DeserializeArray<TObj>(string pJson);
    }
}
