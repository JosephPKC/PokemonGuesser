using LogWrapper;
using PkmDataRetrieval.Retrieval.Models;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval.Controllers.StaticData
{
    internal abstract class BaseStaticDataController(IPkmGateway pApi, ICacheHandler pCache, LoggerFactoryConf pLoggerConf, int pCurrGenId)
        : BaseController(pApi, pCache, pLoggerConf, pCurrGenId)
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
                log.Warn($"Could not get a dict of {nameof(TRet)} from key {pKey}.");
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
                log.Warn($"Could not get a all of {nameof(TRet)}.");
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
                return null;
            }

            Dictionary<string, TRet> retDict = [];
            foreach (string resUrl in allRets.Select(x => x.ResUrl))
            {
                TRet? retModel = GetRetByResUrl<TRet>(resUrl);
                if (retModel is null)
                {
                    continue;
                }

                if (!retDict.TryAdd(resUrl, retModel))
                {
                    log.Warn($"Trying to add url {resUrl}. Ignoring...");
                }
            }

            return retDict;
        }
    }
}
