namespace PkmApi.Utils
{
    public interface ICacheFactory
    {
        ICache<TData> BuildCache<TData>(long? pSizeLimit = null, int? pLifeInSec = null);
    }
}
