namespace PkmApi.Utils
{
    //  Builds a basic cache. Public so that users can use the basic cache if they don't have their own implementation.
    public class CacheFactory : ICacheFactory
    {
        #region ICacheFactory
        public ICache<TData> BuildCache<TData>(long? pSizeLimit = null, int? pLifeInSec = null)
        {
            return new Cache<TData>(pSizeLimit, pLifeInSec);
        }
        #endregion
    }
}
