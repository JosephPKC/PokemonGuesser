using PkmDataRetrieval.Retrieval.Models;
using PkmDataRetrieval.Utils.Cache;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal abstract class BaseStaticDataController(IPkmGateway pApi, ICacheHandler pCache, int pCurrGenId)
        : BaseController(pApi, pCache, pCurrGenId)
    {
        protected IDictionary<string, TRet>? GetRetDict<TRet>(string pKey, Func<IDictionary<string, TRet>?> pGetFromApi) where TRet : BaseRetModel
        {
            IDictionary<string, TRet>? model = _cache.Get<IDictionary<string, TRet>>(pKey);
            if (model is not null)
            {
                return model;
            }

            model = pGetFromApi();
            if (model is null)
            {
                //  WARN
                return null;
            }

            _cache.Add(pKey, model);
            return model;
        }

        protected IEnumerable<BasicRetModel>? GetAllRet<TRet>() where TRet : BaseRetModel
        {
            string key = $"{GetRetKeyPrefix<TRet>()}:all";
            IEnumerable<BasicRetModel>? model = _cache.Get<IEnumerable<BasicRetModel>>(key);
            if (model is not null)
            {
                return model;
            }

            model = _api.GetAll<TRet>();
            if (model is null)
            {
                //  WARN
                return null;
            }

            _cache.Add(key, model);
            return model;
        }

        protected IDictionary<string, TRet>? GetAllRetAsDict<TRet>() where TRet : BaseRetModel
        {
            IEnumerable<BasicRetModel>? allRets = GetAllRet<TRet>();
            if (allRets is null)
            {
                //  WARN
                return null;
            }

            Dictionary<string, TRet> retDict = [];
            foreach (string resUrl in allRets.Select(x => x.ResUrl))
            {
                TRet? retModel = GetRetByResUrl<TRet>(resUrl);
                if (retModel is null)
                {
                    //  WARN
                    continue;
                }

                if (!retDict.TryAdd(resUrl, retModel))
                {
                    //  WARN
                }
            }

            return retDict;
        }
    }
}
