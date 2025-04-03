namespace PkmApi.Utils
{
    public interface ICache<TData>
    {
        bool Add(string pKey, TData pData, int? pLifeInSec = null, bool pOverwrite = false);
        void Clear();
        TData? Get(string pKey);
    }
}
