namespace PkmDataRetrieval.Utils.Cache
{
    public interface ICacheHandler
    {
        bool Add<TData>(string pKey, TData pData, int? pLifeInSec = null, bool pOverwrite = false) where TData : class;
        TData? Get<TData>(string pKey) where TData : class;
    }
}
