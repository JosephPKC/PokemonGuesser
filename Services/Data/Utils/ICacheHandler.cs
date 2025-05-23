namespace Data.Utils
{
    public interface ICacheHandler
    {
        bool Add<TItem>(string pKey, TItem pItem, int? pLifeInSec = null, bool pOverwrite = false) where TItem : class;
        TItem? Get<TItem>(string pKey) where TItem : class;
    }
}
