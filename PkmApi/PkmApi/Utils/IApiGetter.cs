namespace PkmApi.Utils
{
    public interface IApiGetter
    {
        string Get(string pUri);
        Task<string> GetAsync(string pUri);
    }
}
