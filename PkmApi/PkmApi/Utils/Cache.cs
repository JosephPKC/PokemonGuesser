using System.IO.Pipelines;
using Microsoft.Extensions.Caching.Memory;

namespace PkmApi.Utils
{
    //  TODO: Just like the other utils, this will be in a separate utils proj later on that this proj references via NuGet
    //  Look to improving this for TData = value types, esp with the nullability/etc
    internal class Cache<TData> : ICache<TData>, IDisposable
    {
        private MemoryCache _cache;
        private readonly long _sizeLimit = 1024;
        private readonly int _defaultLifeInSec = 300;

        public Cache(long? pSizeLimit = null, int? pLifeInSec = null)
        {
            if (pSizeLimit != null)
            {
                _sizeLimit = pSizeLimit.Value;
            }
   
            if (pLifeInSec != null)
            {
                _defaultLifeInSec = pLifeInSec.Value;
            }
            
            _cache = new(new MemoryCacheOptions() { SizeLimit = _sizeLimit });
        }

        #region ICache<TData>
        public bool Add(string pKey, TData pData, int? pLifeInSec = null, bool pOverwrite = false)
        {
            if (string.IsNullOrWhiteSpace(pKey))
            {
                return false;
            }

            TData? item = Get(pKey);
            if (!EqualityComparer<TData>.Default.Equals(item, default) && !pOverwrite)
            {
                return false;
            }

            MemoryCacheEntryOptions memOptions = new()
            {
                Size = 1,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(pLifeInSec ?? _defaultLifeInSec)
            };

            _ = _cache.Set(pKey, pData, memOptions);
            return true;
        }

        public void Clear()
        {
            _cache.Dispose();
            _cache = new MemoryCache(new MemoryCacheOptions() { SizeLimit = _sizeLimit });
        }

        public TData? Get(string pKey)
        {
            if (string.IsNullOrWhiteSpace(pKey))
            {
                return default;
            }

            return _cache.Get<TData>(pKey);
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _cache.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
